﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notatnik.Server.Data;
using Notatnik.Shared;
using System;
using System.Security.Cryptography;

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

            var notes = await _context.Notes.Where(p => p.Public == true && p.Secure == false).ToListAsync();
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
        public async Task<ServiceResponse<List<NoteDisplayDto>>> GetNotesByUser(string username)
        {
            var response = new ServiceResponse<List<NoteDisplayDto>>();

            var user = await _context.Users.Include(d => d.Notes).FirstOrDefaultAsync(i => i.Username == username);
            var notes = user.Notes;
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
        public async Task<ServiceResponse<NoteDisplayDto>> GetNoteDetails(int id, string password, string username)
        {
            var response = new ServiceResponse<NoteDisplayDto>();
            var user = await _context.Users.Include(d => d.Notes).FirstOrDefaultAsync(i => i.Username == username);

            var note = user.Notes.FirstOrDefault(i => i.NoteId == id);
            var noteDisplayDto = _mapper.Map<NoteDisplayDto>(note);


            if (note == null)
            {
                response.Success = false;
                response.Message = "Note not found.";
                return response;

            }
            if (note.Secure == false)
            {
                response.Data = noteDisplayDto;
                response.Message = "Success, your note is not secure";
                return response;

            }
            if (!VerifyPasswordHash(password, note.NoteHash, note.NoteSalt))
            {
                response.Success = false;
                response.Message = "Bad Credentials";
                return response;

            }
            noteDisplayDto.Body = EncryptDecryptManager.Decrypt(note.Body);
            response.Data = noteDisplayDto;
            response.Message = "Success, your note is secure";
            return response;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
