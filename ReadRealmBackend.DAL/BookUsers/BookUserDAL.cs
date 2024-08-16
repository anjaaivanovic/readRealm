using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Common.Constants;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.BookUsers
{
    public class BookUserDAL : BaseDAL<BookUser>, IBookUserDAL
    {
        public BookUserDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<string?> FavoriteGenreAsync(string userId)
        {
            var genre = await _set
                .Where(bu => bu.UserId == userId)
                .Join(
                    _context.Books, 
                    bu => bu.BookId, 
                    b => b.Id, (bu, b) => b
                )
                .SelectMany(
                    b => b.Genres, 
                    (b, genre) => new 
                    { 
                        Book = b, 
                        Genre = genre 
                    }
                )
                .GroupBy(bg => bg.Genre.Name)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            return genre;
        }

        public async Task<int> TotalBooksReadAsync(string userId)
        {
            var readStatus = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == StringConstants.ReadStatus);
            return await _set.Where(bu => bu.UserId == userId && bu.StatusId == readStatus.Id).CountAsync();
        }

        public async Task<int> TotalBooksRatedAsync(string userId)
        {
            return await _set.Where(bu => bu.UserId == userId && bu.Rating != null).CountAsync();
        }

        public async Task<decimal?> AverageRatingAsync(string userId)
        {
            return await _set.Where(bu => bu.UserId == userId && bu.Rating != null).AverageAsync(bu => bu.Rating);
        }
    }
}