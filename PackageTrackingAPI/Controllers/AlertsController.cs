using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DTOs;

namespace Package_Tracking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly AlertService _alertService;

        public AlertsController(AlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlertDto>>> GetAlerts()
        {
            return await _alertService.GetAllAlertsAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<AlertDto>>> GetAlertsByUser(int userId)
        {
            return await _alertService.GetAlertsByUserIdAsync(userId);
        }

        [HttpPost]
        public async Task<ActionResult<AlertDto>> CreateAlert(AlertDto alertDto)
        {
            var newAlert = await _alertService.CreateAlertAsync(alertDto);
            return CreatedAtAction(nameof(GetAlertsByUser), new { userId = newAlert.UserID }, newAlert);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            await _alertService.DeleteAlertAsync(id);
            return NoContent();
        }
    }
}
