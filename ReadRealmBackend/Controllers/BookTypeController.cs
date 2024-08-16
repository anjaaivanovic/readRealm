using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Authors;
using ReadRealmBackend.BL.BookTypes;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Requests.BookTypes;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/bookType")]
    public class BookTypeController : Controller
    {
        private readonly IBookTypeBL _bookTypeBL;

        public BookTypeController(IBookTypeBL bookTypeBL)
        {
            _bookTypeBL = bookTypeBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetBookTypeAsync(int id)
        {
            return Ok(await _bookTypeBL.GetBookTypeAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetBookTypesAsync()
        {
            return Ok(await _bookTypeBL.GetBookTypesAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertBookTypeAsync(InsertBookTypeRequest req)
        {
            return Ok(await _bookTypeBL.InsertBookTypeAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateBookTypeAsync(UpdateBookTypeRequest req)
        {
            return Ok(await _bookTypeBL.UpdateBookTypeAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteBookTypeAsync(int id)
        {
            return Ok(await _bookTypeBL.DeleteBookTypeAsync(id));
        }

        #endregion
    }
}
