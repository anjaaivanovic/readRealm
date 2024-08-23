using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Books
{
    public interface IBookBL
    {
        Task<GenericResponse<BookResponse?>> GetBookAsync(int id, string userId);
        Task<GenericResponse<GenericPaginationResponse<RecommendedBookResponse>>> GetBooksAsync(BookPaginationRequest req, string userId);
        Task<GenericResponse<GenericPaginationResponse<MutualBookResponse>>> GetMutualBooksAsync(BookPaginationRequest req, string userId);
        Task<GenericResponse<string>> InsertBookAsync(InsertBookRequest req);
        Task<GenericResponse<string>> DeleteBookAsync(int id);
    }
}