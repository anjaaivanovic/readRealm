using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.BookTypes
{
    public class BookTypeDAL : BaseDAL<BookType>, IBookTypeDAL
    {
        public BookTypeDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckBookTypeAsync(int id)
        {
            return await _set.AnyAsync(bookType => bookType.Id == id);
        }

        public async Task<bool> CheckBookTypeByNameAsync(string name)
        {
            return await _set.AnyAsync(bookType => bookType.Name == name);
        }
    }
}