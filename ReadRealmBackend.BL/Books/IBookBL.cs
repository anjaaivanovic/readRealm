using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Books;

namespace ReadRealmBackend.BL.Books
{
    public interface IBookBL
    {
        Task<GenericResponse<BookResponse?>> GetBookAsync(int id);
        Task<GenericResponse<string>> InsertBookAsync(InsertBookRequest req);
        Task<GenericResponse<string>> DeleteBookAsync(int id);
    }
}