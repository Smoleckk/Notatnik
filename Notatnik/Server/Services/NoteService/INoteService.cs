using Notatnik.Shared;

namespace Notatnik.Server.Services.NoteService
{
    public interface INoteService
    {
        Task<ServiceResponse<List<NoteDisplayDto>>> GetNotes();
        Task<ServiceResponse<List<NoteDisplayDto>>> GetNotesByUser();
        Task<ServiceResponse<NoteDisplayDto>> GetNoteDetails(int id, Credentials credentials);
    }
}
