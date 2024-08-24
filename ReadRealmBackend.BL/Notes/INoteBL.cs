using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Responses.Generic;
using ReadRealmBackend.Models.Responses.Notes;

namespace ReadRealmBackend.BL.Notes
{
    public interface INoteBL
    {
        Task<GenericResponse<GenericPaginationResponse<NoteResponse>>> GetBookNotesAsync(BookNotePaginationRequest req, string userId);
        Task<GenericResponse<string>> InsertNoteAsync(InsertNoteFullRequest req);
    }
}