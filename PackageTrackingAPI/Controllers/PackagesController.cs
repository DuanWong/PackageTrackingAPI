using Microsoft.AspNetCore.Mvc;
using PackageTrackingAPI.BLL;
using PackageTrackingAPI.DTOs;

namespace PackageTrackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly PackageService _packageService;

        public PackagesController(PackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDto>> GetPackageById(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null) return NotFound();
            return Ok(package);
        }

        [HttpGet("tracking/{trackingNumber}")]
        public async Task<ActionResult<PackageDto>> GetPackageByTrackingNumber(string trackingNumber)
        {
            var package = await _packageService.GetPackageByTrackingNumberAsync(trackingNumber);
            if (package == null) return NotFound();
            return Ok(package);
        }

        [HttpPost]
        public async Task<ActionResult<PackageDto>> CreatePackage([FromBody] PackageDto packageDto)
        {
            var createdPackage = await _packageService.CreatePackageAsync(packageDto);
            return CreatedAtAction(nameof(GetPackageById), new { id = createdPackage.PackageID }, createdPackage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageDto packageDto)
        {
            await _packageService.UpdatePackageAsync(id, packageDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _packageService.DeletePackageAsync(id);
            return NoContent();
        }
    }
}