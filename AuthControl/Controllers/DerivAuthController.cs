using AuthControl.Services.DerivSmartRobot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AuthControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerivAuthController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly DerivClient _derivClient;
        public DerivAuthController(ApplicationContext context, DerivClient derivClient)
        {
            _context = context;
            _derivClient = derivClient;
        }


        [HttpGet("redirect")]
        public async Task<IActionResult> Login([FromQuery] Dictionary<string, string> parameters)
        {
            try
            {
                var accountsAndToken = parameters.Where(x => x.Key.Contains("acct") || x.Key.Contains("token"));
                var acAndTokenAssociated = new Dictionary<string, string>();

                for (int i = 0; i < accountsAndToken.Count() / 2; i++)
                {
                    acAndTokenAssociated.Add(accountsAndToken.FirstOrDefault(x => x.Key.Contains($"acct{i + 1}")).Value, accountsAndToken.FirstOrDefault(x => x.Key.Contains($"token{i + 1}")).Value);
                }

                _derivClient.SetConfigurations("31306");
                _derivClient.Connect();
                _derivClient.Authorize(acAndTokenAssociated.First().Value);

                await Task.Delay(4000);

                if (_derivClient.authorization != null)
                {
                    var user = await _context.User.FirstOrDefaultAsync(x => x.Email == _derivClient.authorization.Authorize.Email);
                    if (user != null)
                    {

                        user.TokensOAuth = JsonConvert.SerializeObject(acAndTokenAssociated);
                        user.TokenDateDeadLine = DateTime.Now.AddHours(24);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return View("ViewError");

                    }

                }

                return View("View");
            }
            catch (Exception e)
            {
                return View("ViewError");
            }
        }
    }
}
