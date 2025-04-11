using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class PackageRepository
    {
        private readonly PackageTrackingContext _context;

        public PackageRepository(PackageTrackingContext context)
        {
            _context = context;
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await _context.Packages
                .Include(p => p.TrackingEvents)
                .Include(p => p.Sender)
                .FirstOrDefaultAsync(p => p.PackageID == id);
        }

        public async Task<Package> GetByTrackingNumberAsync(string trackingNumber)
        {
            return await _context.Packages
                .Include(p => p.TrackingEvents)
                .Include(p => p.Sender)
                .FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);
        }

        public async Task AddAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Package package)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var package = await GetByIdAsync(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Packages.AnyAsync(p => p.PackageID == id);
        }
    }
}