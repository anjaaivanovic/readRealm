using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Common.Constants;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.DAL.Books
{
    public class BookDAL : BaseDAL<Book>, IBookDAL
    {
        private static int recommendationCount = 6;
        private static int noteCount = 10;

        public BookDAL(ReadRealmContext context) : base(context)
        {
        }

        #region Check

        public async Task<bool> CheckBookAsync(int id)
        {
            return await _set.AnyAsync(book => book.Id == id);
        }

        #endregion

        #region Get

        public async Task<Book?> GetBookAsync(int id, string userId)
        {
            var publicVisibilityId = (await _context.NoteVisibilities.FirstOrDefaultAsync(n => n.Name == StringConstants.PublicVisibility)).Id;
            var privateVisibilityId = (await _context.NoteVisibilities.FirstOrDefaultAsync(n => n.Name == StringConstants.PrivateVisibility)).Id;
            var finalThoughtsTypeId = (await _context.NoteTypes.FirstOrDefaultAsync(n => n.Name == StringConstants.FinalNoteType)).Id;

            var bookHelper = await _context.Books
                .Where(b => b.Id == id)
                .Include(b => b.BookUsers)
                .Include(b => b.Type)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Languages)
                .Select(b => new
                {
                    Book = b,
                    Notes = b.Notes
                        .Where(n => n.UserId == userId
                        && n.NoteVisibilityId == privateVisibilityId
                        )
                        .OrderBy(n => n.Chapter)
                        .Take(noteCount),
                    FinalThoughts = b.Notes
                        .Where(n => n.NoteVisibilityId == publicVisibilityId
                            && n.TypeId == finalThoughtsTypeId
                        )
                        .OrderByDescending(n => n.DatePosted)
                        .Take(10)
                })
                .FirstOrDefaultAsync();

            bookHelper.Book.Notes = bookHelper.Notes.ToList();
            bookHelper.Book.FinalThoughts = bookHelper.FinalThoughts.ToList();

            bookHelper.Book.Rating = await _context.BookUsers
                .Where(bu => bu.BookId == id)
                .SumAsync(bu => bu.Rating) ?? 0;

            return bookHelper.Book;
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
            var friendIds = await _context.Friends
                .Where(f => f.FirstUserId == userId || f.SecondUserId == userId)
                .Select(f => f.FirstUserId == userId ? f.SecondUserId : f.FirstUserId)
                .Distinct()
                .ToListAsync();

            var spoilerFreeTypeId = (await _context.NoteTypes.FirstOrDefaultAsync(n => n.Name == StringConstants.SpoilerFree)).Id;
            var currentlyReadingStatusId = (await _context.Statuses.FirstOrDefaultAsync(s => s.Name == StringConstants.ReadingStatus)).Id;
            var privateVisibilityId = (await _context.NoteVisibilities.FirstOrDefaultAsync(nv => nv.Name == StringConstants.PrivateVisibility)).Id;

            return await _context.BookUsers
                .Where(bu => friendIds.Contains(bu.UserId) && bu.StatusId == currentlyReadingStatusId)
                .Join(_set,
                      bu => bu.BookId,
                      b => b.Id,
                      (bu, b) => b)
                .Distinct()
                .Take(recommendationCount)
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.BookUsers)
                .Include(b => b.Notes.Where(n => n.TypeId == spoilerFreeTypeId && n.NoteVisibilityId != privateVisibilityId))
                .ThenInclude(n => n.NoteVisibility)
                .ToListAsync();
        }

        public async Task<GenericPaginationResponse<Book>> GetBooksAsync(BookPaginationRequest req, string userId)
        {
            var query = _set.Include(b => b.Genres).AsQueryable();

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

            if (req.Sort != null && req.IsAsc != null)
            {

                var property = typeof(Book).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(req.Sort));

                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(Book), "b");
                    var propertyAccess = Expression.Property(parameter, property);
                    var orderByExpression = Expression.Lambda<Func<Book, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

                    query = (bool)req.IsAsc
                        ? query.OrderBy(orderByExpression)
                        : query.OrderByDescending(orderByExpression);
                }
            }

            #endregion

            #region Mutual

            if (req.Mutual != null && (bool)req.Mutual)
            {
                var friendIds = await _context.Friends
                   .Where(f => f.FirstUserId == userId || f.SecondUserId == userId)
                   .Select(f => f.FirstUserId == userId ? f.SecondUserId : f.FirstUserId)
                   .Distinct()
                   .ToListAsync();

                var userBooks = await _context.BookUsers
                    .Where(bu => bu.UserId == userId)
                    .Select(bu => bu.BookId)
                    .ToListAsync();

                var booksWithGenres = await query
                    .Include(b => b.Genres)
                    .Where(b => userBooks.Contains(b.Id))
                    .ToListAsync();

                var mutualBooksQuery = booksWithGenres
                    .Join(_context.BookUsers,
                          b => b.Id,
                          bu => bu.BookId,
                          (b, bu) => new { Book = b, BookUser = bu })
                    .Where(ub => friendIds.Contains(ub.BookUser.UserId))
                    .GroupBy(ub => ub.Book)
                    .Select(g => new
                    {
                        Book = g.Key,
                        BookUsers = g.Select(ub => ub.BookUser.UserId).Distinct().Where(uid => uid != userId).ToList()
                    });

                var booksWithUsers = mutualBooksQuery
                    .Skip((req.Page - 1) * req.ItemCount)
                    .Take(req.ItemCount)
                    .ToList();

                var books = booksWithUsers.Select(bwu =>
                {
                    bwu.Book.BookUsers = bwu.BookUsers.Select(userId => new BookUser { UserId = userId }).ToList();
                    return bwu.Book;
                }).ToList();

                return new GenericPaginationResponse<Book>
                {
                    Items = books,
                    TotalItemCount = mutualBooksQuery.Count()
                };
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

            if (req.Sort != null && req.IsAsc != null)
            {
                var property = typeof(Book).GetProperty(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(req.Sort));

                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(UsersBook), "b");
                    var propertyAccess = Expression.Property(Expression.Property(parameter, nameof(UsersBook.Book)), property);
                    var orderByExpression = Expression.Lambda<Func<UsersBook, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

                    query = (bool)req.IsAsc
                        ? query.OrderBy(orderByExpression)
                        : query.OrderByDescending(orderByExpression);
                }
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