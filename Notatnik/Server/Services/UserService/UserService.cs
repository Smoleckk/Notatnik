using Microsoft.EntityFrameworkCore;
using Notatnik.Server.Data;
using Notatnik.Shared;
using System.Security.Cryptography;
using static Azure.Core.HttpHeader;

namespace Notatnik.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<User>> CreateUserNote(NoteDto noteDto, string username)
        {
            var response = new ServiceResponse<User>();

            CreatePasswordHash(noteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            Note note = new Note();

            note.Title = noteDto.Title;

            if (noteDto.Secure)
            {
                note.Body = EncryptDecryptManager.Encrypt(noteDto.Body);
                note.Secure = noteDto.Secure;
                note.Public = false;
                note.NoteHash = passwordHash;
                note.NoteSalt = passwordSalt;
            }
            else
            {
                note.Body = noteDto.Body;
                note.Secure = noteDto.Secure;
                note.Public = noteDto.Public;
                note.NoteHash = passwordHash;
                note.NoteSalt = passwordSalt;
            }


            var user = await _context.Users.Include(d => d.Notes).FirstOrDefaultAsync(i => i.Username == username);
            user.Notes.Add(note);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }
        public async Task<ServiceResponse<Note>> GetNote(int id, string password)
        {
            var response = new ServiceResponse<Note>();

            var note = await _context.Notes.FirstOrDefaultAsync(i => i.NoteId == id);
            if (note == null)
            {
                response.Success = false;
                response.Message = "Note not found.";
                return response;

            }
            if (note.Secure == false)
            {
                response.Data = note;
                response.Message = "Success, your note is not secure";
                return response;

            }
            if (!VerifyPasswordHash(password, note.NoteHash, note.NoteSalt))
            {
                response.Success = false;
                response.Message = "Bad Credentials";
                return response;

            }
            note.Body = EncryptDecryptManager.Decrypt(note.Body);
            response.Data = note;
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
