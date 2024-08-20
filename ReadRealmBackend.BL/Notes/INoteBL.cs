using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.Notes;

namespace ReadRealmBackend.BL.Notes
{
    public interface INoteBL
    {
        Task<GenericResponse<string>> InsertNoteAsync(InsertNoteFullRequest req);
    }
}