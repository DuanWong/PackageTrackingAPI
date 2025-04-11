using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageTrackingAPI.DAL;

namespace PackageTrackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingEventsController : ControllerBase
    {
        private readonly PackageTrackingContext _context;
        private readonly IMapper _mapper;

        public TrackingEventsController(PackageTrackingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<TrackingEventDto>> CreateTrackingEvent([FromBody] TrackingEventDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Status = 400,
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            // Verify package exists
            var packageExists = await _context.Packages.AnyAsync(p => p.PackageID == dto.PackageID);
            if (!packageExists)
            {
                return NotFound(new
                {
                    Status = 404,
                    Message = $"Package with ID {dto.PackageID} not found"
                });
            }

            var trackingEvent = _mapper.Map<TrackingEvent>(dto);
            trackingEvent.Timestamp = DateTime.UtcNow;

            _context.TrackingEvents.Add(trackingEvent);
            await _context.SaveChangesAsync();

            // Partner's alert service would trigger here
            return CreatedAtAction(
                nameof(GetTrackingEvent),
                new { id = trackingEvent.EventID },
                _mapper.Map<TrackingEventDto>(trackingEvent));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackingEventDto>>> GetTrackingEvents(
            [FromQuery] int? packageId = null,
            [FromQuery] string status = null)
        {
            var query = _context.TrackingEvents.AsQueryable();

            if (packageId.HasValue)
                query = query.Where(e => e.PackageID == packageId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(e => e.Status == status);

            var events = await query.ToListAsync();
            return _mapper.Map<List<TrackingEventDto>>(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackingEventDto>> GetTrackingEvent(int id)
        {
            var trackingEvent = await _context.TrackingEvents
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (trackingEvent == null)
            {
                return NotFound(new
                {
                    Status = 404,
                    Message = $"Tracking event with ID {id} not found"
                });
            }

            return _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        [HttpGet("package/{packageId}/latest")]
        public async Task<ActionResult<TrackingEventDto>> GetLatestEvent(int packageId)
        {
            var latestEvent = await _context.TrackingEvents
                .Where(e => e.PackageID == packageId)
                .OrderByDescending(e => e.Timestamp)
                .FirstOrDefaultAsync();

            if (latestEvent == null)
            {
                return NotFound(new
                {
                    Status = 404,
                    Message = $"No events found for package {packageId}"
                });
            }

            return _mapper.Map<TrackingEventDto>(latestEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrackingEvent(int id, [FromBody] TrackingEventDto dto)
        {
            if (id != dto.EventID)
            {
                return BadRequest(new
                {
                    Status = 400,
                    Message = "ID mismatch between URL and body"
                });
            }

            var trackingEvent = await _context.TrackingEvents.FindAsync(id);
            if (trackingEvent == null)
            {
                return NotFound(new
                {
                    Status = 404,
                    Message = $"Tracking event with ID {id} not found"
                });
            }

            _mapper.Map(dto, trackingEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackingEvent(int id)
        {
            var trackingEvent = await _context.TrackingEvents.FindAsync(id);
            if (trackingEvent == null)
            {
                return NotFound(new
                {
                    Status = 404,
                    Message = $"Tracking event with ID {id} not found"
                });
            }

            _context.TrackingEvents.Remove(trackingEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}