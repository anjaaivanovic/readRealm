using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.BookUsers
{
    public interface IBookUserDAL: IBaseDAL<BookUser>
    {
        Task<string?> FavoriteGenreAsync(int userId);
        Task<int> TotalBooksReadAsync(int userId);
        Task<int> TotalBooksRatedAsync(int userId);
        Task<decimal?> AverageRatingAsync(int userId);
    }
}