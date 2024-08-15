using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.FriendRequests
{
    public interface IFriendRequestDAL: IBaseDAL<FriendRequest>
    {
        Task<FriendRequest?> GetFriendRequestAsync(FriendRequest friendRequest);
    }
}