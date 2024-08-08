using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

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
    }
}