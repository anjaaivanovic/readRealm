using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Notes
{
    public interface INoteBL
    {
        Task<GenericResponse<string>> InsertNoteAsync(InsertNoteFullRequest req);
    }
}