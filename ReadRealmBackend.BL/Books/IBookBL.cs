using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Responses.Books;

namespace ReadRealmBackend.BL.Books
{
    public interface IBookBL
    {
        Task<GenericResponse<BookResponse?>> GetBookAsync(int id);
    }
}