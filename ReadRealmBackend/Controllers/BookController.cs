using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Books;
using ReadRealmBackend.Models.Requests.Books;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : Controller
    {
        private readonly IBookBL _bookBL;

        public BookController(IBookBL bookBL)
        {
            _bookBL = bookBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookBL.GetBookAsync(id));
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