using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class TrackingEventRepository
    {
        private readonly PackageTrackingContext _context;

        public TrackingEventRepository(PackageTrackingContext context)
        {
            _context = context;
        }

        public async Task<TrackingEvent> GetByIdAsync(int id)
        {
            return await _context.TrackingEvents
                .Include(te => te.Package)
                .FirstOrDefaultAsync(te => te.EventID == id);
        }

        public async Task<List<TrackingEvent>> GetByPackageIdAsync(int packageId)
        {
            return await _context.TrackingEvents
                .Where(te => te.PackageID == packageId)
                .ToListAsync();
        }

        public async Task<TrackingEvent> GetLatestEventAsync(int packageId)
        {
            return await _context.TrackingEvents
                .Where(te => te.PackageID == packageId)
                .OrderByDescending(te => te.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(TrackingEvent trackingEvent)
        {
            await _context.TrackingEvents.AddAsync(trackingEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrackingEvent trackingEvent)
        {
            _context.TrackingEvents.Update(trackingEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trackingEvent = await GetByIdAsync(id);
            if (trackingEvent != null)
            {
                _context.TrackingEvents.Remove(trackingEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}