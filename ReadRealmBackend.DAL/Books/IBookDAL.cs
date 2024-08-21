using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Books;

namespace ReadRealmBackend.DAL.Books
{
    public interface IBookDAL: IBaseDAL<Book>
    {
        Task<Book?> GetBookAsync(int id);
        Task<List<Book>> GetContinueReadingBooksAsync(string userId);
        Task<List<Book>> GetRecommendedBooksAsync(string userId);
        Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(string userId);
        Task<List<Book>> GetBooksAsync(BookPaginationRequest req);
    }
}