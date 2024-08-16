using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Home;

namespace ReadRealmBackend.BL.Home
{
    public interface IHomeBL
    {
        Task<GenericResponse<HomeStats>> GetStats(string userId);
        Task<GenericResponse<List<ContinueReadingBookResponse>>> GetContinueReadingBooksAsync(string userId);
        Task<GenericResponse<List<RecommendedBookResponse>>> GetRecommendedBookByGenresAsync(string userId);
        Task<GenericResponse<List<RecommendedBookByFriendsActivityResponse>>> GetRecommendedBookByFriendsActivityAsync(string userId);
    }
}