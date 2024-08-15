using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.DAL.Base;
using ReadRealmBackend.Models.Context;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.DAL.FriendRequests
{
    public class FriendRequestDAL : BaseDAL<FriendRequest>, IFriendRequestDAL
    {
        public FriendRequestDAL(ReadRealmContext context) : base(context)
        {
        }

        public async Task<FriendRequest?> GetFriendRequestAsync(FriendRequest friendRequest)
        {
            return await _set.FirstOrDefaultAsync(fr => fr == friendRequest);
        }
    }
}