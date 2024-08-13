using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Friends
{
    public interface IFriendDAL: IBaseDAL<Friend>
    {
        Task<bool> CheckFriendsAsync(int userId);
    }
}