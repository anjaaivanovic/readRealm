using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.Friends
{
    public class FriendDAL : BaseDAL<Friend>, IFriendDAL
    {
        public FriendDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<bool> CheckFriendsAsync(string userId)
        {
            return await _set.AnyAsync(f => f.FirstUserId == userId || f.SecondUserId == userId);
        }
    }
}