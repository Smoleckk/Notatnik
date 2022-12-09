using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notatnik.Server.Data;
using Notatnik.Shared;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace Notatnik.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public AuthService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<User> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User();

            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Notes = new List<Note>();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<ServiceResponse<string>> Login(UserDto request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Username == request.Username);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Bad Credentials";
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Bad Credentials";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }
        //public async Task<ServiceResponse<User>> CreateUserNote(NoteDto noteDto, string username)
        //{
        //    var response = new ServiceResponse<User>();

        //    CreatePasswordHash(noteDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        //    Note note = new Note();

        //    note.Title = noteDto.Title;

        //    if (noteDto.Secure)
        //    {
        //        note.Body = EncryptDecryptManager.Encrypt(noteDto.Body);
        //        note.Secure = noteDto.Secure;
        //        note.Public = false;
        //        note.NoteHash = passwordHash;
        //        note.NoteSalt = passwordSalt;
        //    }
        //    else
        //    {
        //        note.Body = noteDto.Body;
        //        note.Secure = noteDto.Secure;
        //        note.Public = noteDto.Public;
        //        note.NoteHash = passwordHash;
        //        note.NoteSalt = passwordSalt;
        //    }


        //    var user = await _context.Users.Include(d => d.Notes).FirstOrDefaultAsync(i => i.Username == username);
        //    user.Notes.Add(note);

        //    _context.Users.Update(user);
        //    await _context.SaveChangesAsync();
        //    response.Data = user;
        //    return response;
        //}
        //public async Task<ServiceResponse<Note>> GetNote(int id, string password)
        //{
        //    var response = new ServiceResponse<Note>();

        //    var note = await _context.Notes.FirstOrDefaultAsync(i => i.NoteId == id);
        //    if (note == null)
        //    {
        //        response.Success = false;
        //        response.Message = "Note not found.";
        //        return response;

        //    }
        //    if (note.Secure == false)
        //    {
        //        response.Data = note;
        //        response.Message = "Success, your note is not secure";
        //        return response;

        //    }
        //    if (!VerifyPasswordHash(password, note.NoteHash, note.NoteSalt))
        //    {
        //        response.Success = false;
        //        response.Message = "Bad Credentials";
        //        return response;

        //    }
        //    note.Body = EncryptDecryptManager.Decrypt(note.Body);
        //    response.Data = note;
        //    response.Message = "Success, your note is secure";
        //    return response;

        //}

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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
