﻿using Microsoft.AspNetCore.Mvc;
using Notatnik.Shared;

namespace Notatnik.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserRegisterRequest>> Register(UserRegisterRequest request);
        Task<ServiceResponse<string>> Login(UserLoginCaptcha request);
        Task<ServiceResponse<string>> Verify(string token);
    }
}
