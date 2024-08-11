using ReadRealmBackend.Models.Requests.BookTypes;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Responses.BookTypes;

namespace ReadRealmBackend.BL.BookTypes
{
    public interface IBookTypeBL
    {
        Task<GenericResponse<BookTypeResponse>> GetBookTypeAsync(int id);
        Task<GenericResponse<List<BookTypeResponse>>> GetBookTypesAsync();
        Task<GenericResponse<string>> InsertBookTypeAsync(InsertBookTypeRequest req);
        Task<GenericResponse<string>> UpdateBookTypeAsync(UpdateBookTypeRequest req);
        Task<GenericResponse<string>> DeleteBookTypeAsync(int id);
    }
}