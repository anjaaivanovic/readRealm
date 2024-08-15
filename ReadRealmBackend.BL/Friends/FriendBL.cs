using AutoMapper;
using ReadRealmBackend.DAL.FriendRequests;
using ReadRealmBackend.DAL.Friends;
using ReadRealmBackend.Models;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.BL.Friends
{
    public class FriendBL: IFriendBL
    {
        private readonly IFriendDAL _friendDAL;
        private readonly IFriendRequestDAL _friendRequestDAL;
        private readonly IMapper _mapper;

        public FriendBL(IFriendDAL friendDAL, IFriendRequestDAL friendRequestDAL, IMapper mapper)
        {
            _friendDAL = friendDAL;
            _friendRequestDAL = friendRequestDAL;
            _mapper = mapper;
        }

        public async Task<GenericResponse<string>> InsertFriendRequestAsync(FriendRequest req)
        {
            await _friendRequestDAL.InsertOneAsync(req);
            var success = await _friendRequestDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted friend request!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }

        public async Task<GenericResponse<string>> InsertFriendAsync(FriendRequest req)
        {
            var friendRequest = await _friendRequestDAL.GetFriendRequestAsync(req);

            if (friendRequest == null)
            {
                return new GenericResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Invalid friend request!" }
                };
            }


            await _friendDAL.InsertOneAsync(_mapper.Map<FriendRequest, Friend>(req));
            var success = await _friendRequestDAL.SaveAsync();

            if (success)
            {
                return new GenericResponse<string>
                {
                    Success = success,
                    Data = "Successfully inserted friend!"
                };
            }

            return new GenericResponse<string>
            {
                Success = success,
                Errors = new List<string> { "Changes could not be saved!" }
            };
        }
    }
}