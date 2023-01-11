using Microsoft.EntityFrameworkCore;
using Notatnik.Server.Data;
using Notatnik.Shared;
using Notatnik.Shared.Dtos.NoteDto;
using Notatnik.Shared.Models;
using System.Security.Cryptography;
using static Azure.Core.HttpHeader;

namespace Notatnik.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<NoteDto>> CreateUserNote(NoteDto noteDto)
        {
            var response = new ServiceResponse<NoteDto>();

            CreatePasswordHash(noteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            Note note = new Note
            {
                Title = noteDto.Title
            };

            if (noteDto.Secure)
            {
                //note.Body = EncryptDecryptManager.Encrypt(noteDto.Body);
                note.Body = EncryptDecryptGCM.Encrypt(noteDto.Body);

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

            string username = GetUser();
            var user = await _context.Users.Include(d => d.Notes).FirstOrDefaultAsync(i => i.Username == username);
            user.Notes.Add(note);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            response.Data = noteDto;
            return response;
        }

        public string GetUser()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            }
            return result;
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
