using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class AlertRepository
    {
        private readonly PackageTrackingContext _context;

        public AlertRepository(PackageTrackingContext context)
        {
            _context = context;
        }

        public async Task<List<Alert>> GetAllAlertsAsync()
        {
            return await _context.Alerts.ToListAsync();
        }

        public async Task<List<Alert>> GetAlertsByUserIdAsync(int userId)
        {
            return await _context.Alerts.Where(a => a.UserID == userId).ToListAsync();
        }

        public async Task<Alert> GetAlertByIdAsync(int alertId)
        {
            return await _context.Alerts.FirstOrDefaultAsync(a => a.AlertID == alertId);
        }

        public async Task AddAlertAsync(Alert alert)
        {
            await _context.Alerts.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlertAsync(int alertId)
        {
            var alert = await _context.Alerts.FindAsync(alertId);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }
    }
}
