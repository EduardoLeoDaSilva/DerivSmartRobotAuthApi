using AuthControl.Entities;
using AuthControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthControl.Services
{
    public class UserService
    {

        private readonly ApplicationContext _context;
        private readonly EmailService _emailService;
        public UserService(ApplicationContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<UserBase> LogIn(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            if (user.Password == new EncriptionService().Encript(password))
                return user;

            return null;

        }


        public async Task<UserBase> GetUser(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;


            return user;

        }

        public async Task<bool> UpdatePassword(string email, string password)
        {

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return false;

            user.Password = new EncriptionService().Encript(password);

            _context.Update(user);

            await _context.SaveChangesAsync();

            return true;

        }


        public async Task<bool> ResendEmail()
        {

            var users = await _context.RetryQueues.ToListAsync();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    try
                    {
                     var result =   await _emailService.SendEmail(user.Email, user.Password);

                     if(result.IsSuccessStatusCode)
                         _context.RetryQueues.Remove(user);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return true;

        }


        public async Task<bool> UpdateConfig(ConfigView config)
        {

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == config.Email);

            if (user == null)
                return false;

            //user.AccountType = config.AccountType;

            //if (user.AccountType == AccountType.Real)
            //{
            //    user.ApiTokenReal = config.ApiToken;

            //}
            //else if (user.AccountType == AccountType.Demo)
            //{
            //    user.ApiTokenDemo = config.ApiToken;
            //}

            //_context.Update(user);

            await _context.SaveChangesAsync();

            return true;

        }
    }
}
