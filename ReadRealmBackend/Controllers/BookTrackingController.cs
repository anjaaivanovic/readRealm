using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.BookUsers;
using ReadRealmBackend.Models.Requests.BookAuthors;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/bookTracking")]
    public class BookTrackingController : Controller
    {
        private readonly IBookUserBL _bookUserBL;

        public BookTrackingController(IBookUserBL bookUserBL)
        {
            _bookUserBL = bookUserBL;
        }

        [HttpPost]
        public async Task<IActionResult> InsertBookUserAsync(InsertBookUserRequest req)
        {
            return Ok(await _bookUserBL.InsertBookUserAsync(req));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookUserAsync(InsertBookUserRequest req)
        {
            return Ok(await _bookUserBL.UpdateBookUserAsync(req));
        }
    }
}