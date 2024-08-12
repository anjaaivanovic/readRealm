using AutoMapper;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.BookUsers;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Home;

namespace ReadRealmBackend.BL.Home
{
    public class HomeBL : IHomeBL
    {
        private readonly IBookUserDAL _bookUserDAL;
        private readonly IBookDAL _bookDAL;
        private readonly IMapper _mapper;

        public HomeBL(IBookUserDAL bookUserDAL, IMapper mapper, IBookDAL bookDAL)
        {
            _bookUserDAL = bookUserDAL;
            _mapper = mapper;
            _bookDAL = bookDAL;
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

        public async Task<GenericResponse<List<ContinueReadingBookResponse>>> GetContinueReadingBooksAsync(int userId)
        {
            var resp = await _bookDAL.GetContinueReadingBooksAsync(userId);

            return new GenericResponse<List<ContinueReadingBookResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<Book>, List<ContinueReadingBookResponse>>(resp)
            };
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
