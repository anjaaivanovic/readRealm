using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Responses.Authors;

namespace ReadRealmBackend.BL.Authors
{
    public interface IAuthorBL
    {
        Task<GenericResponse<AuthorResponse>> GetAuthorAsync(int id);
        Task<GenericResponse<List<AuthorResponse>>> GetAuthorsAsync();
        Task<GenericResponse<string>> InsertAuthorAsync(InsertAuthorRequest req);
        Task<GenericResponse<string>> UpdateAuthorAsync(UpdateAuthorRequest req);
        Task<GenericResponse<string>> DeleteAuthorAsync(int id);
    }
}