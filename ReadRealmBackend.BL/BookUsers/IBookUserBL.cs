using ReadRealmBackend.Models.Requests.BookUsers;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.BookUsers
{
    public interface IBookUserBL
    {
        Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserFullRequest req);
        Task<GenericResponse<string>> UpdateBookUserAsync(InsertBookUserFullRequest req);
    }
}