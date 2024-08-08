using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Books;

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

        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _bookBL.GetBookAsync(id));
        }
    }
}