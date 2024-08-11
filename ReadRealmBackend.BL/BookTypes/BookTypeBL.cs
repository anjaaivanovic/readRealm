using AutoMapper;
using ReadRealmBackend.DAL.BookTypes;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Requests.BookTypes;
using ReadRealmBackend.Models.Responses.BookTypes;

namespace ReadRealmBackend.BL.BookTypes
{
    public class BookTypeBL: IBookTypeBL
    {
        private readonly IBookTypeDAL _bookTypeDAL;
        private readonly IMapper _mapper;

        public BookTypeBL(IBookTypeDAL bookTypeDAL, IMapper mapper)
        {
            _bookTypeDAL = bookTypeDAL;
            _mapper = mapper;
        }

        #region Get

        public async Task<GenericResponse<BookTypeResponse>> GetBookTypeAsync(int id)
        {
            var bookType = await _bookTypeDAL.GetOneAsync(id);

            if (bookType == null)
            {
                return new GenericResponse<BookTypeResponse>
                {
                    Success = false,
                    Errors = new List<string> { "No book type with such id!" }
                };
            }

            return new GenericResponse<BookTypeResponse>
            {
                Success = true,
                Data = _mapper.Map<BookType, BookTypeResponse>(bookType)
            };
        }

        public async Task<GenericResponse<List<BookTypeResponse>>> GetBookTypesAsync()
        {
            var bookTypes = await _bookTypeDAL.GetAllAsync();

            return new GenericResponse<List<BookTypeResponse>>
            {
                Success = true,
                Data = _mapper.Map<List<BookType>, List<BookTypeResponse>>(bookTypes)
            };
        }

        #endregion

        #region Insert

        public async Task<GenericResponse<string>> InsertBookTypeAsync(InsertBookTypeRequest req)
        {
            if (await _bookTypeDAL.CheckBookTypeByNameAsync(req.Name))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Book type already exists!" }
                };
            }

            await _bookTypeDAL.InsertOneAsync(_mapper.Map<InsertBookTypeRequest, BookType>(req));
            var success = await _bookTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted book type!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion

        #region Update

        public async Task<GenericResponse<string>> UpdateBookTypeAsync(UpdateBookTypeRequest req)
        {
            if (!await _bookTypeDAL.CheckBookTypeAsync(req.Id))
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No book type with such id!" }
                };
            }

            _bookTypeDAL.UpdateOne(_mapper.Map<UpdateBookTypeRequest, BookType>(req));
            var success = await _bookTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully updated book type!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion

        #region Delete

        public async Task<GenericResponse<string>> DeleteBookTypeAsync(int id)
        {
            var toDelete = await _bookTypeDAL.GetOneAsync(id);

            if (toDelete == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "No book type with such id!" }
                };
            }

            _bookTypeDAL.DeleteOne(toDelete);
            var success = await _bookTypeDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully deleted book type!"
                };
            }
            else
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Errors = new List<string> { "Changes could not be saved!" }
                };
            }
        }

        #endregion
    }
}