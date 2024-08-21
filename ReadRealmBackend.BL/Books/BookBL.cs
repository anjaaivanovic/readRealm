using AutoMapper;
using ReadRealmBackend.DAL.Authors;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.BookTypes;
using ReadRealmBackend.DAL.Genres;
using ReadRealmBackend.DAL.Languages;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Responses.Books;
using ReadRealmBackend.Models.Responses.Generic;

namespace ReadRealmBackend.BL.Books
{
    public class BookBL: IBookBL
    {   
        private readonly IBookDAL _bookDAL;
        private readonly IBookTypeDAL _bookTypeDAL;
        private readonly IAuthorDAL _authorDAL;
        private readonly IGenreDAL _genreDAL;
        private readonly ILanguageDAL _languageDAL;
        private readonly IMapper _mapper;

        public BookBL(IBookDAL bookDAL, IMapper mapper, IBookTypeDAL bookTypeDAL, IGenreDAL genreDAL, ILanguageDAL languageDAL, IAuthorDAL authorDAL)
        {
            _bookDAL = bookDAL;
            _mapper = mapper;
            _bookTypeDAL = bookTypeDAL;
            _genreDAL = genreDAL;
            _languageDAL = languageDAL;
            _authorDAL = authorDAL;
        }

        public async Task<GenericResponse<BookResponse?>> GetBookAsync(int id)
        {
            var book = await _bookDAL.GetBookAsync(id);

            if (book == null)
            {
                return new GenericResponse<BookResponse?>
                {
                    Success = false,
                    Errors = new List<string> { "No book with such id!" }
                };
            }

            var mappedBook = _mapper.Map<Book, BookResponse>(book);

            return new GenericResponse<BookResponse?>
            {
                Data = mappedBook,
                Success = true
            };
        }

        public async Task<GenericResponse<GenericPaginationResponse<RecommendedBookResponse>>> GetBooksAsync(BookPaginationRequest req)
        {
            var data = new GenericPaginationResponse<RecommendedBookResponse>
            {
                Items = _mapper.Map<List<RecommendedBookResponse>>(await _bookDAL.GetBooksAsync(req)),
                TotalItemCount = await _bookDAL.GetTotalCountAsync()
            };

            return new GenericResponse<GenericPaginationResponse<RecommendedBookResponse>>
            {
                Data = data,
                Success = true
            };
        }


        public async Task<GenericResponse<string>> InsertBookAsync(InsertBookRequest req)
        {
            #region Validation

            if (!(req.AuthorIds.Any() && req.GenreIds.Any() && req.LanguageIds.Any()))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Authors, genres or languages are not specified! " }
                };
            }

            if (!await _bookTypeDAL.CheckBookTypeAsync(req.TypeId))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "The specified book type does not exist! " }
                };
            }

            var authors = await _authorDAL.GetMultipleAuthorsAsync(req.AuthorIds);
            var genres = await _genreDAL.GetMultipleGenresAsync(req.GenreIds);
            var languages = await _languageDAL.GetMultipleLanguagesAsync(req.LanguageIds);

            var missingAuthors = req.AuthorIds.Except(authors.Select(a => a.Id)).ToList();
            var missingGenres = req.GenreIds.Except(genres.Select(g => g.Id)).ToList();
            var missingLanguages = req.LanguageIds.Except(languages.Select(l => l.Id)).ToList();

            if (missingAuthors.Any())
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "The following author ids do not exist: " + string.Join(", ", missingAuthors) }
                };
            }

            if (missingGenres.Any())
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "The following genre ids do not exist: " + string.Join(", ", missingGenres) }
                };
            }

            if (missingLanguages.Any())
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "The following language ids do not exist: " + string.Join(", ", missingLanguages) }
                };
            }

            #endregion

            var book = _mapper.Map<InsertBookRequest, Book>(req);
            book.Authors = authors;
            book.Genres = genres;
            book.Languages = languages;

            await _bookDAL.InsertOneAsync(book);
            var success = await _bookDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted book!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        public async Task<GenericResponse<string>> DeleteBookAsync(int id)
        {
            var toDelete = await _bookDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No book with such id!" }
                };
            }

            _bookDAL.DeleteOne(toDelete);
            var success = await _bookDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted book!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }
    }
}