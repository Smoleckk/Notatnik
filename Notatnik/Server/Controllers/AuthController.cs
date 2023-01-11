using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notatnik.Server.Services.AuthService;
using Notatnik.Server.Services.UserService;
using Notatnik.Shared;
using Notatnik.Shared.Dtos.UserDto;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ValidateAntiForgeryToken]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet("GetMe"), Authorize(Roles = "User")]
        public ActionResult<string> GetMe()
        {
            var username = _userService.GetUser();
            return Ok(username);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegisterRequest request)
        {
            var response = await _authService.Register(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginCaptcha request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
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
