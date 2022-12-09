using Notatnik.Shared;

namespace Notatnik.Server.Services.NoteService
{
    public interface INoteService
    {
        Task<ServiceResponse<List<NoteDisplayDto>>> GetNotes();
    }
}
