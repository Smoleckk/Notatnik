using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notatnik.Server.Data;
using Notatnik.Shared;
using System;

namespace Notatnik.Server.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NoteService(IConfiguration config, DataContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<NoteDisplayDto>>> GetNotes()
        {
            var response = new ServiceResponse<List<NoteDisplayDto>>();

            var notes = await _context.Notes.ToListAsync();
            //.Where(p => p.Public == true && p.Secure == false)
            if (notes == null)
            {
                response.Success = false;
                response.Message = "Notes not found.";
                return response;
            }
            List<NoteDisplayDto> notesDto = new List<NoteDisplayDto>();
            foreach (var note in notes)
            {
                var noteDisplayDto = _mapper.Map<NoteDisplayDto>(note);
                notesDto.Add(noteDisplayDto);
            }

            response.Data = notesDto;
            return response;
        }

    }
}
