using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Books;
using ReadRealmBackend.Models.Requests.Books;
using ReadRealmBackend.Models.Requests.BookUsers;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookBL _bookBL;
        private readonly IMapper _mapper;

        public BookController(IBookBL bookBL)
        {
            _bookBL = bookBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _bookBL.GetBookAsync(id, userId));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetBooksAsync([FromQuery]BookPaginationRequest req)
        {
            return Ok(await _bookBL.GetBooksAsync(req));
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertBookAsync(InsertBookRequest req)
        {
            return Ok(await _bookBL.InsertBookAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            return Ok(await _bookBL.DeleteBookAsync(id));
        }

        #endregion
    }
}