using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.NoteTypes;
using ReadRealmBackend.Models.Requests.NoteTypes;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/noteType")]
    public class NoteTypeController : Controller
    {
        private readonly INoteTypeBL _noteTypeBL;

        public NoteTypeController(INoteTypeBL noteTypeBL)
        {
            _noteTypeBL = noteTypeBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetNoteTypeAsync(int id)
        {
            return Ok(await _noteTypeBL.GetNoteTypeAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetNoteTypesAsync()
        {
            return Ok(await _noteTypeBL.GetNoteTypesAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertNoteTypeAsync(InsertNoteTypeRequest req)
        {
            return Ok(await _noteTypeBL.InsertNoteTypeAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateNoteTypeAsync(UpdateNoteTypeRequest req)
        {
            return Ok(await _noteTypeBL.UpdateNoteTypeAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteNoteTypeAsync(int id)
        {
            return Ok(await _noteTypeBL.DeleteNoteTypeAsync(id));
        }

        #endregion
    }
}
