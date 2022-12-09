using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notatnik.Server.Services.UserService;
using Notatnik.Shared;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user-note")]
        public async Task<ActionResult<ServiceResponse<User>>> CreateUserNote(NoteDto noteDto, string username)
        {
            var response = await _userService.CreateUserNote(noteDto, username);
            if (!response.Success)
            {
                return BadRequest("Bad");
            }

            return Ok(response.Data);
        }
        [HttpGet("id")]
        public async Task<ActionResult<Note>> GetNote(int id, string password)
        {
            var response = await _userService.GetNote(id, password);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
