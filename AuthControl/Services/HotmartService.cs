using AuthControl.Entities;
using AuthControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthControl.Services
{
    public class HotmartService
    {
        private readonly ApplicationContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<HotmartService> _logger;

        public HotmartService(ApplicationContext context, EmailService emailService, ILogger<HotmartService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task ProcessOrderAprovedCompletedEvent(OrderEvent order)
        {

            if (order == null)
                return;

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == order.Data.Buyer.Email);

            if (user != null)
            {
                user.Robots = string.Join(';', Plans.GetRobotsBaseOnPlan(order.Data.Subscription.Plan.Name, _context)); //todo 

                _context.User.Update(user);

                await _context.SaveChangesAsync();
                return;

            }

            var password = RandomString(6);

            user = new UserBase
            {
                Id = Guid.NewGuid(),
                Email = order.Data.Buyer.Email,
                Password = new EncriptionService().Encript(password),
                Robots = string.Join(';', Plans.GetRobotsBaseOnPlan(order.Data.Subscription.Plan.Name, _context)),
                Active = true
            };


            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            try
            {
              var result =  await _emailService.SendEmail(order.Data.Buyer.Email, password);

              if (!result.IsSuccessStatusCode)
              {
                  await _context.RetryQueues.AddAsync(new RetryQueue
                      { Email = order.Data.Buyer.Email, Password = password, Id = Guid.NewGuid() });
                  await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                await _context.RetryQueues.AddAsync(new RetryQueue
                    {Email = order.Data.Buyer.Email, Password = password, Id = Guid.NewGuid()});
                await _context.SaveChangesAsync();
                _logger.LogCritical(e, "Erro ao enviar email");
                
            }

        }


        public async Task ProcessOrderCanceledEstornedEvent(OrderEvent order)
        {
            if (order == null)
                return;

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == order.Data.Subscriber.Email);

            if (user == null)
                return;

            user.Robots = string.Empty;


            if (user != null)
            {

                user.Active = false;

                _context.User.Update(user);

                await _context.SaveChangesAsync();

            }

        }


        public async Task ProcessOrderChangedPlanEvent(RootChangePlan order)
        {


            if (order == null)
                return;

            var changedPlan = order.Data.Plans.FirstOrDefault(x => x.Current == true);

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == order.Data.Subscription.User.Email);

            if (user != null)
            {

                user.Active = true;

                user.Robots = string.Empty;

                user.Robots = string.Join(';', Plans.GetRobotsBaseOnPlan(changedPlan.Name, _context));//todo

                _context.User.Update(user);

                await _context.SaveChangesAsync();

            }

        }


        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
