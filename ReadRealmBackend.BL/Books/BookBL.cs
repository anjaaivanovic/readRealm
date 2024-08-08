using AutoMapper;
using ReadRealmBackend.DAL.Books;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models.Responses.Books;

namespace ReadRealmBackend.BL.Books
{
    public class BookBL: IBookBL
    {   
        private readonly IBookDAL _bookDAL;
        private readonly IMapper _mapper;
        public BookBL(IBookDAL bookDAL, IMapper mapper)
        {
            _bookDAL = bookDAL;
            _mapper = mapper;
        }

        public async Task<GenericResponse<BookResponse?>> GetBookAsync(int id)
        {
            var book = await _bookDAL.GetBookAsync(id);
            var mappedBook = _mapper.Map<Book, BookResponse>(book);

            return new GenericResponse<BookResponse?>
            {
                Data = mappedBook,
                Success = true
            };
        }
    }
}