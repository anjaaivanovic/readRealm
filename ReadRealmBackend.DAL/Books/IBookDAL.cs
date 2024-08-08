using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Books
{
    public interface IBookDAL: IBaseDAL<Book>
    {
        Task<Book?> GetBookAsync(int id);
    }
}
