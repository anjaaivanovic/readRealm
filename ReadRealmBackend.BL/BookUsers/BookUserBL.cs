using AutoMapper;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.BookUsers;
using ReadRealmBackend.DAL.Statuses;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.BookAuthors;

namespace ReadRealmBackend.BL.BookUsers
{
    public class BookUserBL: IBookUserBL
    {
        private readonly IBookUserDAL _bookUserDAL;
        private readonly IBookDAL _bookDAL;
        private readonly IStatusDAL _statusDAL;
        private readonly IMapper _mapper;

        public BookUserBL(IBookUserDAL bookUserDAL, IBookDAL bookDAL, IStatusDAL statusDAL, IMapper mapper)
        {
            _bookUserDAL = bookUserDAL;
            _bookDAL = bookDAL;
            _statusDAL = statusDAL;
            _mapper = mapper;
        }

        public async Task<GenericResponse<string>> InsertBookUserAsync(InsertBookUserRequest req)
        {
            #region Validation

            var book = await _bookDAL.GetOneAsync(req.BookId);

            if (book == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No book with such id!" }
                };
            }

            if (book.ChapterCount < req.CurrentChapter)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Invalid chapter value!" }
                };
            }

            if (!await _statusDAL.CheckStatusAsync(req.StatusId))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No status with such id!" }
                };
            }

            #endregion

            await _bookUserDAL.InsertOneAsync(_mapper.Map<InsertBookUserRequest, BookUser>(req));
            var success = await _bookUserDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted book-user link!"
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