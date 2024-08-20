using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.BookUsers;
using ReadRealmBackend.BL.Notes;
using ReadRealmBackend.Models.Requests.BookAuthors;
using ReadRealmBackend.Models.Requests.Notes;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/bookTracking")]
    public class BookTrackingController : Controller
    {
        private readonly IBookUserBL _bookUserBL;
        private readonly INoteBL _noteBL;

        public BookTrackingController(IBookUserBL bookUserBL, INoteBL noteBL)
        {
            _bookUserBL = bookUserBL;
            _noteBL = noteBL;
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

        [HttpPost("note")]
        public async Task<IActionResult> InsertNoteAsync(InsertNoteRequest req)
        {
            return Ok(await _noteBL.InsertNoteAsync(req));
        }
    }
}