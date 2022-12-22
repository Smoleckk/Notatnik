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
        const int MaxNumberOfFailedAttemptsToLogin = 10;
        const int BlockMinutesAfterLimitFailedAttemptsToLogin = 10;
        public AuthService(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<ServiceResponse<UserRegisterRequest>> Register(UserRegisterRequest request)
        {
            User user = new User();
            var response = new ServiceResponse<UserRegisterRequest>();

            if (_context.Users.Any(u => u.Email == request.Email || u.Username == request.Username))
            {
                response.Success = false;
                response.Message = "This email or username is taken";
                response.Data = request;
                return response;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = request.Email;
            user.Username = request.Username;
            user.VerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Notes = new List<Note>();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = "User registered successfullt";
            response.Data = request;
            return response;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginCaptcha request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == request.Email);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Wrong credentials";
                return response;
            }

            user.LoginLastTry = DateTime.Now;

            if (user.LoginLastTry < user.LoginBlockUntil)
            {
                response.Success = false;
                response.Message = "Too many try login, you account is blocked until" + user.LoginBlockUntil.ToString();
                return response;
            }
            if (user.NumberOfLoginTry > MaxNumberOfFailedAttemptsToLogin)
            {
                user.LoginBlockUntil = DateTime.Now.AddMinutes(BlockMinutesAfterLimitFailedAttemptsToLogin);
                response.Success = false;
                response.Message = "Too many try login, you account is blocked until " + user.LoginBlockUntil.ToString();
                user.NumberOfLoginTry = 0;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return response;
            }


            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong credentials";
                user.NumberOfLoginTry++;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                response.Message = "Token created successfully";
                response.Data = CreateToken(user);
                user.NumberOfLoginTry = 0;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return response;
        }
        public async Task<ServiceResponse<string>> Verify(string token)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(i => i.VerificationToken == token);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid token";
            }
            response.Success = true;
            response.Message = "User verified";
            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return response;
        }
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
