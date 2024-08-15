using ReadRealmBackend.Models.Entities;
using ReadRealmBackend.Models;

namespace ReadRealmBackend.BL.Friends
{
    public interface IFriendBL
    {
        Task<GenericResponse<string>> InsertFriendRequestAsync(FriendRequest req);
        Task<GenericResponse<string>> InsertFriendAsync(FriendRequest req);
    }
}