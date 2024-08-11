using Microsoft.AspNetCore.Mvc;
using ReadRealmBackend.BL.Statuses;
using ReadRealmBackend.Models.Requests.Statuses;

namespace ReadRealmBackend.API.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : Controller
    {
        private readonly IStatusBL _statusBL;

        public StatusController(IStatusBL statusBL)
        {
            _statusBL = statusBL;
        }

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetStatusAsync(int id)
        {
            return Ok(await _statusBL.GetStatusAsync(id));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetStatusesAsync()
        {
            return Ok(await _statusBL.GetStatusesAsync());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> InsertStatusAsync(InsertStatusRequest req)
        {
            return Ok(await _statusBL.InsertStatusAsync(req));
        }

        #endregion

        #region PUT

        [HttpPut]
        public async Task<IActionResult> UpdateStatusAsync(UpdateStatusRequest req)
        {
            return Ok(await _statusBL.UpdateStatusAsync(req));
        }

        #endregion

        #region DELETE

        [HttpDelete]
        public async Task<IActionResult> DeleteStatusAsync(int id)
        {
            return Ok(await _statusBL.DeleteStatusAsync(id));
        }

        #endregion
    }
}
