using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Home;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IHomeBL _homeBL;

        public HomeController(IHomeBL homeBL)
        {
            _homeBL = homeBL;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats(int userId)
        {
            return Ok(await _homeBL.GetStats(userId));
        }

        [HttpGet("continueReading")]
        public async Task<IActionResult> GetContinueReadingBooksAsync(int userId)
        {
            return Ok(await _homeBL.GetContinueReadingBooksAsync(userId));
        }

        [HttpGet("friendsReading")]
        public async Task<IActionResult> GetRecommendedBookByFriendsActivityAsync(int userId)
        {
            return Ok(await _homeBL.GetRecommendedBookByFriendsActivityAsync(userId));
        }

        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommendedBookByGenresAsync(int userId)
        {
            return Ok(await _homeBL.GetRecommendedBookByGenresAsync(userId));
        }
    }
}