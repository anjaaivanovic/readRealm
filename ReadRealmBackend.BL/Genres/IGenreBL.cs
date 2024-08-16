using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Requests.Genres;
using ReadRealmBackend.Models.Responses.Genres;

namespace ReadRealmBackend.BL.Genres
{
    public interface IGenreBL
    {
        Task<GenericResponse<GenreResponse>> GetGenreAsync(int id);
        Task<GenericResponse<List<GenreResponse>>> GetGenresAsync();
        Task<GenericResponse<string>> InsertGenreAsync(InsertGenreRequest req);
        Task<GenericResponse<string>> UpdateGenreAsync(UpdateGenreRequest req);
        Task<GenericResponse<string>> DeleteGenreAsync(int id);
    }
}