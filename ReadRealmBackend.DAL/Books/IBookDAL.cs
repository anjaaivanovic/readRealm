using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Books
{
    public interface IBookDAL: IBaseDAL<Book>
    {
        Task<Book?> GetBookAsync(int id);
        Task<List<Book>> GetContinueReadingBooksAsync(string userId);
        Task<List<Book>> GetRecommendedBooksAsync(string userId);
        Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(string userId);
    }
}