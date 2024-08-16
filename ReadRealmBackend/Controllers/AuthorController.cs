using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Authors;
using ReadRealmBackend.Models.Requests.Authors;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : Controller
    {
        private readonly IAuthorBL _authorBL;

        public AuthorController(IAuthorBL authorBL)
        {
            _authorBL = authorBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAuthorAsync(int id)
        {
            return Ok(await _authorBL.GetAuthorAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            return Ok(await _authorBL.GetAuthorsAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertAuthorAsync(InsertAuthorRequest req)
        {
            return Ok(await _authorBL.InsertAuthorAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateAuthorAsync(UpdateAuthorRequest req)
        {
            return Ok(await _authorBL.UpdateAuthorAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthorAsync(int id)
        {
            return Ok(await _authorBL.DeleteAuthorAsync(id));
        }

        #endregion
    }
}
