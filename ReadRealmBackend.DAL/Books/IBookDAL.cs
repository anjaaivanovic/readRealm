using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Books
{
    public interface IBookDAL: IBaseDAL<Book>
    {
        Task<Book?> GetBookAsync(int id);
        Task<List<Book>> GetContinueReadingBooksAsync(int userId);
        Task<List<Book>> GetRecommendedBooksAsync(int userId);
        Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(int userId);
    }
}