using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Books
{
    public interface IBookBL
    {
        Task<GenericResponse<BookResponse?>> GetBookAsync(int id);
        Task<GenericResponse<GenericPaginationResponse<RecommendedBookResponse>>> GetBooksAsync(BookPaginationRequest req);
        Task<GenericResponse<string>> InsertBookAsync(InsertBookRequest req);
        Task<GenericResponse<string>> DeleteBookAsync(int id);
    }
}