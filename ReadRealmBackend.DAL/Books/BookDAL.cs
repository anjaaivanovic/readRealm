using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Common.Constants;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.DAL.Books
{
    public class BookDAL : BaseDAL<Book>, IBookDAL
    {
        private static int recommendationCount = 6;

        public BookDAL(ReadRealmContext context) : base(context)
        {
        }

        #region Get

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

        public async Task<List<Book>> GetContinueReadingBooksAsync(string userId)
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

        public async Task<List<Book>> GetRecommendedBooksAsync(string userId)
        {
            var usersGenres = await _context.BookUsers
             .Where(bu => bu.UserId == userId)
             .Join(_set, bu => bu.BookId, b => b.Id, (bu, b) => b)
             .SelectMany(b => b.Genres.Select(g => g.Id))
             .ToListAsync();

            foreach (var user in usersGenres)
            {
                Console.WriteLine(user);
            }
            var usersBooks = await _context.BookUsers
               .Where(bu => bu.UserId == userId)
               .Select(bu => bu.BookId)
               .ToListAsync();

            if (usersGenres.Any())
            {
                return await _set
                  .Where(b => b.Genres.Any(g => usersGenres.Contains(g.Id)) && !usersBooks.Contains(b.Id))
                  .Take(recommendationCount)
                  .ToListAsync();
            }
            else
            {
                return await _set
                .OrderBy(b => Guid.NewGuid())
                .Take(recommendationCount)
                .ToListAsync();
            }
        }

        public async Task<List<Book>> GetRecommendedBooksByFriendsActivityAsync(string userId)
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

        public async Task<GenericPaginationResponse<Book>> GetBooksAsync(BookPaginationRequest req)
        {
            var query = _set.AsQueryable();

            #region Filter

            if (req.MinWordCount != null)
            {
                query = query.Where(book => book.WordCount >= req.MinWordCount);
            }

            if (req.MaxWordCount != null)
            {
                query = query.Where(book => book.WordCount <= req.MaxWordCount);
            }

            if (req.BookTypeId != null)
            {
                query = query.Where(book => book.TypeId == req.BookTypeId);
            }

            if (req.AuthorId != null)
            {
                query = query.Where(book => book.Authors.Any(author => author.Id == req.AuthorId));
            }

            if (req.GenreId != null)
            {
                query = query.Where(book => book.Genres.Any(genre => genre.Id == req.GenreId));
            }

            if (req.LanguageId != null)
            {
                query = query.Where(book => book.Languages.Any(language => language.Id == req.LanguageId));
            }

            #endregion

            #region Search

            if (req.Search != null)
            {
                query = query.Where(book => 
                    book.Title.Contains(req.Search.Trim()) 
                    || book.BriefDescription.Contains(req.Search.Trim())
                );
            }

            #endregion

            #region Sort

            var property = typeof(Book).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(req.Sort));

            if (property != null)
            {
                var parameter = Expression.Parameter(typeof(Book), "b");
                var propertyAccess = Expression.Property(parameter, property);
                var orderByExpression = Expression.Lambda<Func<Book, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

                query = req.IsAsc
                    ? query.OrderBy(orderByExpression)
                    : query.OrderByDescending(orderByExpression);
            }

            #endregion

            return new GenericPaginationResponse<Book>
            {
                Items = await query
                .Include(book => book.Genres)
                .Skip((req.Page - 1) * req.ItemCount)
                .Take(req.ItemCount)
                .ToListAsync(),
                TotalItemCount = await query.CountAsync()
            };
        }

        public async Task<GenericPaginationResponse<UsersBook>> GetUsersBooksAsync(UsersBookPaginationRequest req, string userId)
        {
            var query = _context.BookUsers
                .Where(bookUser => bookUser.UserId == userId && bookUser.StatusId == req.StatusId)
                .Include(bu => bu.Book)
                .ThenInclude(b => b.Authors)
                .Select(bu => new UsersBook
                {
                    BookUser = bu,
                    Book = bu.Book
                });

            #region Sort

            var property = typeof(Book).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(req.Sort));

            if (property != null)
            {
                var parameter = Expression.Parameter(typeof(UsersBook), "b");
                var propertyAccess = Expression.Property(Expression.Property(parameter, nameof(UsersBook.Book)), property);
                var orderByExpression = Expression.Lambda<Func<UsersBook, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

                query = req.IsAsc
                    ? query.OrderBy(orderByExpression)
                    : query.OrderByDescending(orderByExpression);
            }

            #endregion

            return new GenericPaginationResponse<UsersBook>
            {
                Items = await query
                    .Skip((req.Page - 1) * req.ItemCount)
                    .Take(req.ItemCount)
                    .ToListAsync(),
                TotalItemCount = await query.CountAsync()
            };
        }

        #endregion
    }
}