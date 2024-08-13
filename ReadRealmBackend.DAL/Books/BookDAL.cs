using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Common.Constants;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Books
{
    public class BookDAL : BaseDAL<Book>, IBookDAL
    {
        private static int recommendationCount = 6;

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
            var readingStatus = (await _context.Statuses.FirstOrDefaultAsync(s => s.Name == StringConstants.ReadingStatus)).Id;

            return await _set
                .Include(b => b.BookUsers)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Where(b => 
                    b.BookUsers.Any(bu => 
                        bu.UserId == userId 
                        && bu.BookId == b.Id
                        && bu.StatusId == readingStatus
                    )
                )
                .Take(recommendationCount)
                .ToListAsync();
        }

        public async Task<List<Book>> GetRecommendedBooksAsync(int userId)
        {
            var usersGenres = await _context.BookUsers
                .Where(bu => bu.UserId == userId)
                .Join(_set, bu => bu.BookId, b => b.Id, (bu, b) => b)
                .Select(b => b.Genres.Select(g => g.Id))
                .ToListAsync();

            var usersBooks = await _context.BookUsers
               .Where(bu => bu.UserId == userId)
               .Select(bu => bu.BookId)
               .ToListAsync();

            if (usersGenres.Any())
            {
                return await _set
                    .Where(b => usersGenres.Contains(b.Genres.Select(g => g.Id)) && !usersBooks.Contains(b.Id))
                    .ToListAsync();
            }
            else
            {
                return await _set
                    .DistinctBy(b => b.Genres)
                    .ToListAsync();
            }
        }

        public async Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(int userId)
        {
            var friends = await _context.Friends
                .Where(f => f.FirstUserId == userId || f.SecondUserId == userId)
                .ToListAsync();

            var friendIds = friends
                .Select(f => f.FirstUserId == userId ? f.SecondUserId : f.FirstUserId)
                .Distinct()
                .ToList();

            var usersBooks = await _context.BookUsers
                .Where(bu => bu.UserId == userId)
                .Select(bu => bu.BookId)
                .ToListAsync();

            var currentThoughtsTypeId = (await _context.NoteTypes.FirstOrDefaultAsync(n => n.Name == StringConstants.CurrentThoughtsType)).Id;

            return await _context.BookUsers
                .Where(bu => !usersBooks.Contains(bu.BookId) && friendIds.Contains(bu.UserId))
                .Join(_set,
                      bu => bu.BookId,
                      b => b.Id,
                      (bu, b) => b)
                .Where(b => b.Notes.Any(n => n.TypeId == currentThoughtsTypeId && n.BookId == b.Id))
                .Distinct()
                .Take(recommendationCount)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.BookUsers)
                .Include(b => b.Notes)
                .ToListAsync();
        }
    }
}