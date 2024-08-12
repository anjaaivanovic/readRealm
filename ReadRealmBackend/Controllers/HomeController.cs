using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Authors;
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
    }
}
