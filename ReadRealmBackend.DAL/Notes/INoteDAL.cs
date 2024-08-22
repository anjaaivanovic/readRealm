using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.DAL.Notes
{
    public interface INoteDAL: IBaseDAL<Note>
    {
        Task<GenericPaginationResponse<Note>> GetBookNotesAsync(BookNotePaginationRequest req, string userId);
    }
}