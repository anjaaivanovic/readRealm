using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Responses.Books;

namespace ReadRealmBackend.DAL.Books
{
    public class BookDAL : BaseDAL<Book>, IBookDAL
    {
        public BookDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _set
                .Include(b => b.BookUsers)
                .Include(b => b.Notes)
                .Include(b => b.Type)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Languages)
                .FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<List<Book>> GetContinueReadingBooksAsync(int userId)
        {
            return await _set
                .Include(b => b.BookUsers)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Where(b => b.BookUsers.Any(bu => bu.UserId == userId && bu.BookId == b.Id))
                .ToListAsync();
        }

        public Task<List<Book>> GetRecommendedBooksAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}