using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notatnik.Server.Services.NoteService;
using Notatnik.Shared;
using System;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDisplayDto>>> GetNotes()
        {
            var response = await _noteService.GetNotes();
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response.Data.ToArray());
        }
    }
}
