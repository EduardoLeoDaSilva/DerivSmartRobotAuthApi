using AuthControl.Entities;
using AuthControl.Models.Response;
using AuthControl.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        public UserController(UserService service, IConfiguration configuration, ApplicationContext context)
        {
            _service = service;
            _configuration = configuration;
            _context = context;
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
                    return Ok(new BaseResponse<UserBase> { Data = userDb, Success = true, Errors = new List<string>() }) ;

                }
                var response = new BaseResponse<UserBase> { Data = null, Success = false, Errors = new List<string> { "Usuário não encontrado" } };
                return BadRequest(response);
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


        [HttpGet("Robots")]
        [Authorize]
        public async Task<IActionResult> GetRobots()
        {
            try
            {
                var robots = await _context.Robots.ToListAsync();

                if (robots != null)
                {
                    return Ok(new BaseResponse<List<Robots>> { Data = robots, Success = true, Errors = new List<string>() });
                }
                else
                {
                    return Ok(new BaseResponse<List<Robots>> { Data = null, Success = false, Errors = new List<string> { "Erro ao tentar obter robôs" } });

                }


            }
            catch (Exception e)
            {
                return Ok(new BaseResponse<List<Robots>> { Data = null, Success = false, Errors = new List<string> { "Erro interno ao tentar obter robôs, contate o administrador" } });

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

                    if( userDb.TokenDateDeadLine > DateTime.Now)
                    {
                        return Ok(new BaseResponse<UserBase> { Data = userDb, Success = true, Errors = new List<string>() });

                    }
                    else
                    {
                        return Ok(new BaseResponse<UserBase> { Data = null, Success = false, Errors = new List<string> { "OAuth Expirado" } });

                    }

                }

                    var response = new BaseResponse<UserBase> { Data = null, Success = false, Errors = new List<string> { "Usuário não encontrado" } };
                return Ok(response);
                

            }
            catch (Exception e)
            {
                return BadRequest(new BaseResponse<UserBase> { Data = null, Success = false, Errors = new List<string> { "Ocorreu um erro interno, contate o administrador"} });
            }
        }

    }
}
