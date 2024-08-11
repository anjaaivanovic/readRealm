using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Statuses
{
    public interface IStatusDAL: IBaseDAL<Status>
    {
        Task<bool> CheckStatusAsync(int id);
        Task<bool> CheckStatusByNameAsync(string name);
    }
}