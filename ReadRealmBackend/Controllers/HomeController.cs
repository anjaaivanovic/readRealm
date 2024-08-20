using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Home;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeBL _homeBL;

        public HomeController(IHomeBL homeBL)
        {
            _homeBL = homeBL;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _homeBL.GetStats(userId));
        }

        [HttpGet("continueReading")]
        public async Task<IActionResult> GetContinueReadingBooksAsync()
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _homeBL.GetContinueReadingBooksAsync(userId));
        }

        [HttpGet("friendsReading")]
        public async Task<IActionResult> GetRecommendedBookByFriendsActivityAsync()
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _homeBL.GetRecommendedBookByFriendsActivityAsync(userId));
        }

        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommendedBookByGenresAsync()
        {
            var userId = HttpContext.Items["userId"] as string;
            return Ok(await _homeBL.GetRecommendedBookByGenresAsync(userId));
        }
    }
}