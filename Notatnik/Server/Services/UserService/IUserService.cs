using Notatnik.Shared;

namespace Notatnik.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> CreateUserNote(NoteDto noteDto, string username);
        Task<ServiceResponse<Note>> GetNote(int id, string password);
    }
}
