using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.BookUsers
{
    public interface IBookUserDAL: IBaseDAL<BookUser>
    {
        Task<string?> FavoriteGenreAsync(string userId);
        Task<int> TotalBooksReadAsync(string userId);
        Task<int> TotalBooksRatedAsync(string userId);
        Task<decimal?> AverageRatingAsync(string userId);
        Task<BookUser?> GetOneAsync(string userId, int bookId);
        Task<bool> CheckBookUserAsync(int bookId, string userId);
    }
}