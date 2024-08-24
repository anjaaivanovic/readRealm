using AutoMapper;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.DAL.Notes;
using ReadRealmBackend.DAL.NoteTypes;
using ReadRealmBackend.DAL.NoteVisibilities;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.Notes;
using ReadRealmBackend.Models.Responses.Generic;
using ReadRealmBackend.Models.Responses.Notes;

namespace ReadRealmBackend.BL.Notes
{
    public class NoteBL : INoteBL
    {
        private readonly INoteDAL _noteDAL;
        private readonly IBookDAL _bookDAL;
        private readonly INoteTypeDAL _noteTypeDAL;
        private readonly INoteVisibilityDAL _noteVisibilityDAL;
        private readonly IMapper _mapper;

        public NoteBL(INoteDAL noteDAL, IBookDAL bookDAL, IMapper mapper, INoteTypeDAL noteTypeDAL, INoteVisibilityDAL noteVisibilityDAL)
        {
            _noteDAL = noteDAL;
            _bookDAL = bookDAL;
            _mapper = mapper;
            _noteTypeDAL = noteTypeDAL;
            _noteVisibilityDAL = noteVisibilityDAL;
        }

        public async Task<GenericResponse<GenericPaginationResponse<NoteResponse>>> GetBookNotesAsync(BookNotePaginationRequest req, string userId)
        {
            if (!await _bookDAL.CheckBookAsync(req.BookId))
            {
                return new GenericResponse<GenericPaginationResponse<NoteResponse>>
                {
                    Success = false,
                    Errors = new List<string> { "No book wih such id!" }
                };
            }

            if (!await _noteVisibilityDAL.CheckNoteVisibilityAsync(req.Visibility))
            {
                return new GenericResponse<GenericPaginationResponse<NoteResponse>>
                {
                    Success = false,
                    Errors = new List<string> { "No visibility type wih such id!" }
                };
            }

            return new GenericResponse<GenericPaginationResponse<NoteResponse>>
            {
                Success = true,
                Data = _mapper.Map<GenericPaginationResponse<NoteResponse>>(await _noteDAL.GetBookNotesAsync(req, userId))
            };
        }

        public async Task<GenericResponse<string>> InsertNoteAsync(InsertNoteFullRequest req)
        {
            #region Validation

            var book = await _bookDAL.GetOneAsync(req.BookId);

            if (book == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string>() { "No book with such id!" }
                };
            }

            if (book.ChapterCount < req.Chapter)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string>() { "No such chapter!" }
                };
            }

            var type = await _noteTypeDAL.GetOneAsync(req.TypeId);

            if (type == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string>() { "No such note type!" }
                };
            }

            #endregion

            await _noteDAL.InsertOneAsync(_mapper.Map<Note>(req));
            var success = await _noteDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted note!"
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