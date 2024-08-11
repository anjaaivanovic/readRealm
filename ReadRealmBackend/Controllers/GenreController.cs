using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Genres;
using ReadRealmBackend.Models.Requests.Genres;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : Controller
    {
        private readonly IGenreBL _genreBL;

        public GenreController(IGenreBL genreBL)
        {
            _genreBL = genreBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetAuthorAsync(int id)
        {
            return Ok(await _genreBL.GetGenreAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            return Ok(await _genreBL.GetGenresAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertGenreAsync(InsertGenreRequest req)
        {
            return Ok(await _genreBL.InsertGenreAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateGenreAsync(UpdateGenreRequest req)
        {
            return Ok(await _genreBL.UpdateGenreAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            return Ok(await _genreBL.DeleteGenreAsync(id));
        }

        #endregion
    }
}
