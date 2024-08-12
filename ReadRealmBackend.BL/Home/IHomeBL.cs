using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Home;

namespace ReadRealmBackend.BL.Home
{
    public interface IHomeBL
    {
        Task<GenericResponse<HomeStats>> GetStats(int userId);
        Task<GenericResponse<List<ContinueReadingBookResponse>>> GetContinueReadingBooksAsync(int userId);
        Task<GenericResponse<List<RecommendedBookResponse>>> GetRecommendedBookByGenresAsync(int userId);
        Task<GenericResponse<List<RecommendedBookByFriendsActivityResponse>>> GetRecommendedBookByFriendsActivityAsync(int userId);
    }
}