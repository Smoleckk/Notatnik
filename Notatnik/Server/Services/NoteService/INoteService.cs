using Notatnik.Shared;

namespace Notatnik.Server.Services.NoteService
{
    public interface INoteService
    {
        Task<ServiceResponse<List<NoteDisplayDto>>> GetNotes();
        Task<ServiceResponse<List<NoteDisplayDto>>> GetNotesByUser(string username);
        Task<ServiceResponse<NoteDisplayDto>> GetNoteDetails(int id, string password, string username);
    }
}
