using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Authors;
using ReadRealmBackend.BL.Languages;
using ReadRealmBackend.Models.Requests.Authors;
using ReadRealmBackend.Models.Requests.Languages;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/language")]
    public class LanguageController : Controller
    {
        private readonly ILanguageBL _languageBL;

        public LanguageController(ILanguageBL languageBL)
        {
            _languageBL = languageBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetLanguageAsync(int id)
        {
            return Ok(await _languageBL.GetLanguageAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            return Ok(await _languageBL.GetLanguagesAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertLanguageAsync(InsertLanguageRequest req)
        {
            return Ok(await _languageBL.InsertLanguageAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateLanguageAsync(UpdateLanguageRequest req)
        {
            return Ok(await _languageBL.UpdateLanguageAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteLanguageAsync(int id)
        {
            return Ok(await _languageBL.DeleteLanguageAsync(id));
        }

        #endregion
    }
}
