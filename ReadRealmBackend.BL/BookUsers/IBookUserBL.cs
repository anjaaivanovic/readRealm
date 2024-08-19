using ReadRealmBackend.Models.Requests.BookAuthors;
using ReadRealmBackend.Models;

namespace ReadRealmBackend.BL.BookUsers
{
    public interface IBookUserBL
    {
        Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserRequest req);
        Task<GenericResponse<string>> UpdateBookUserAsync(InsertBookUserRequest req);
    }
}