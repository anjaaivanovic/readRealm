using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.BookUsers;

namespace ReadRealmBackend.BL.BookUsers
{
    public interface IBookUserBL
    {
        Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserFullRequest req);
        Task<GenericResponse<string>> UpdateBookUserAsync(InsertBookUserFullRequest req);
    }
}