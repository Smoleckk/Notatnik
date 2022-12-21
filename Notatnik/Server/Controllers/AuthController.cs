using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notatnik.Server.Services.AuthService;
using Notatnik.Shared;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterRequest request)
        {
            var user = await _authService.Register(request);
            return Ok(user);

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginCaptcha request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
            {
                return BadRequest("Bad Credentials");
            }

            return Ok(response.Data);
        }
        //[HttpPost("verify")]
        //public async Task<ActionResult<ServiceResponse<string>>> Verify(string token)
        //{
        //    var response = await _authService.Verify(token);
        //    if (!response.Success)
        //    {
        //        return BadRequest("Bad Credentials");
        //    }

        //    return Ok(response.Data);
        //}
    }
}
