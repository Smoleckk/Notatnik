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
        //public static User user = new User();
        //private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await _authService.Register(request);
            return Ok(user);

        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
            {
                return BadRequest("Bad Credentials");
            }

            return Ok(response.Data);
        }
        //[HttpPost("create-user-note")]
        //public async Task<ActionResult<ServiceResponse<User>>> CreateUserNote(NoteDto noteDto, string username)
        //{
        //    var response = await _authService.CreateUserNote(noteDto, username);
        //    if (!response.Success)
        //    {
        //        return BadRequest("Bad");
        //    }

        //    return Ok(response.Data);
        //}
        //[HttpGet("id")]
        //public async Task<ActionResult<Note>> GetNote(int id, string password)
        //{
        //    var response = await _authService.GetNote(id, password);
        //    if (!response.Success)
        //    {
        //        return NotFound(response.Message);
        //    }
        //    return Ok(response.Data);
        //}

    }
}
