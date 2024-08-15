using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Friends;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/friend")]
    public class FriendController : Controller
    {
        private readonly IFriendBL _friendBL;

        public FriendController(IFriendBL friendBL)
        {
            _friendBL = friendBL;
        }

        [HttpPost("request")]
        public async Task<IActionResult> InsertFriendRequest(FriendRequest req)
        {
            return Ok(await _friendBL.InsertFriendRequestAsync(req));
        }

        [HttpPost()]
        public async Task<IActionResult> InsertFriendAsync(FriendRequest req)
        {
            return Ok(await _friendBL.InsertFriendAsync(req));
        }
    }
}
