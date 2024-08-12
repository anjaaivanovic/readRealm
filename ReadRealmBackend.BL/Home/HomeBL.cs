using ReadRealmBackend.DAL.BookUsers;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Home;

namespace ReadRealmBackend.BL.Home
{
    public class HomeBL : IHomeBL
    {
        private readonly IBookUserDAL _bookUserDAL;

        public HomeBL(IBookUserDAL bookUserDAL)
        {
            _bookUserDAL = bookUserDAL;
        }

        public async Task<GenericResponse<HomeStats>> GetStats(int userId)
        {
            var stats = new HomeStats();
            stats.TotalBooksRead = await _bookUserDAL.TotalBooksReadAsync(userId);
            stats.TotalBooksRated = await _bookUserDAL.TotalBooksRatedAsync(userId);
            stats.FavoriteGenre = await _bookUserDAL.FavoriteGenreAsync(userId) ?? "None";
            stats.AverageRating = await _bookUserDAL.AverageRatingAsync(userId) ?? 0;

            return new GenericResponse<HomeStats>
            {
                Data = stats,
                Success = true
            };
        }

        public Task<GenericResponse<List<ContinueReadingBookResponse>>> GetContinueReadingBooksAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<RecommendedBookByFriendsActivityResponse>>> GetRecommendedBookByFriendsActivityAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<RecommendedBookResponse>>> GetRecommendedBookByGenresAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
