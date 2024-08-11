using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.BookTypes
{
    public interface IBookTypeDAL: IBaseDAL<BookType>
    {
        Task<bool> CheckBookTypeAsync(int id);
        Task<bool> CheckBookTypeByNameAsync(string name);
    }
}