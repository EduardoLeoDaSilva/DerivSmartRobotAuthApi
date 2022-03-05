using AuthControl.Entities;
using AuthControl.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _configuration;
        public UserController(UserService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserView user)
        {
            try
            {
                var userDb = await _service.LogIn(user.Email, user.Password);
                var secreetkey = _configuration.GetSection("JwtAuth").GetValue<string>("SecretKey");

                if (userDb != null)
                {
                    var token =TokenService.GenerateToken(userDb, secreetkey);
                    userDb.JwtToken = token;
                    return Ok(userDb);

                }

                return BadRequest("Usuário não encontrado");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <returns></returns>
        [HttpPost("changePassowrd")]
        public async Task<IActionResult> ChangePassword([FromBody] UserView user)
        {
            try
            {
               var ok = await _service.UpdatePassword(user.Email, user.Password);

                if (ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("resendEmail")]
        public async Task<IActionResult> ResendPendingEmails()
        {
            try
            {
                var ok = await _service.ResendEmail();

                if (ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("saveConfig")]
        [Authorize]
        public async Task<IActionResult> SaveConfig([FromBody] ConfigView config)
        {
            try
            {
                var ok = await _service.UpdateConfig(config);

                if (ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        

        /// <returns></returns>
        [HttpGet("OAuthData")]
        [Authorize]
        public async Task<IActionResult> OAuthData([FromQuery] string email)
        {
            try
            {
                var userDb = await _service.GetUser(email);

                if (userDb != null)
                {
                    return Ok(userDb);

                }

                return BadRequest("Usuário não encontrado");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}
