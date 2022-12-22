using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notatnik.Server.Services.NoteService;
using Notatnik.Shared;
using System;

namespace Notatnik.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    //[ValidateAntiForgeryToken]

    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<List<NoteDisplayDto>>> GetNotes()
        {
            var response = await _noteService.GetNotes();
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response.Data.ToArray());
        }

        [HttpGet("notes-by-user")]
        public async Task<ActionResult<List<NoteDisplayDto>>> GetNotesByUser()
        {
            var response = await _noteService.GetNotesByUser();
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response.Data.ToArray());
        }

        [HttpPost("note-by-user")]
        public async Task<ActionResult<NoteDisplayDto>> GetNote(int id, Credentials credentials)
        {
            var response = await _noteService.GetNoteDetails(id, credentials);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response.Data);
        }
    }
}
