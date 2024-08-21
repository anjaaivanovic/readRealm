using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Requests.BookUsers;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.BookUsers
{
    public interface IBookUserBL
    {
        Task<GenericResponse<GenericPaginationResponse<UsersBookResponse>>> GetUsersBooksAsync(UsersBookPaginationRequest req, string userId);
        Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserFullRequest req);
        Task<GenericResponse<string>> UpdateBookUserAsync(InsertBookUserFullRequest req);
    }
}