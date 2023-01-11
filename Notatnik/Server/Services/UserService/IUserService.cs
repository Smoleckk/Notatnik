using Notatnik.Shared;
using Notatnik.Shared.Dtos.NoteDto;

namespace Notatnik.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<NoteDto>> CreateUserNote(NoteDto noteDto);

        string GetUser();

    }
}
