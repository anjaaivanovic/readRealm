using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Authors
{
    public interface IAuthorDAL : IBaseDAL<Author>
    {
        Task<bool> CheckAuthorAsync(int id);
        Task<bool> CheckAuthorByFullNameAsync(string fullName);
    }
}