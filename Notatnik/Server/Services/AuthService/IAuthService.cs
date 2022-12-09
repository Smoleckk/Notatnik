using Microsoft.AspNetCore.Mvc;
using Notatnik.Shared;

namespace Notatnik.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(UserDto request);
        Task<ServiceResponse<string>> Login(UserDto request);
        //Task<ServiceResponse<User>> CreateUserNote(NoteDto noteDto, string username);
        //Task<ServiceResponse<Note>> GetNote(int id, string password);
    }
}
