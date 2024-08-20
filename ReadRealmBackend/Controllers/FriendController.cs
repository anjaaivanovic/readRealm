using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Friends;
using ReadRealmBackend.Models.Entities;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/friend")]
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendBL _friendBL;

        public FriendController(IFriendBL friendBL)
        {
            _friendBL = friendBL;
        }

        [HttpPost("request")]
        public async Task<IActionResult> InsertFriendRequest(string friendId)
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _friendBL.InsertFriendRequestAsync(new FriendRequest { SenderUserId = userId, ReceiverUserId = friendId }));
        }

        [HttpPost()]
        public async Task<IActionResult> InsertFriendAsync(string friendId)
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _friendBL.InsertFriendAsync(new FriendRequest { SenderUserId = userId, ReceiverUserId = friendId }));
        }
    }
}
