using Microsoft.AspNetCore.Mvc;
using Notatnik.Shared;

namespace Notatnik.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(UserRegisterRequest request);
        Task<ServiceResponse<string>> Login(UserLoginRequest request);
        Task<ServiceResponse<string>> Verify(string token);
    }
}
