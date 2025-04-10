using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class AlertService
    {
        private readonly AlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public AlertService(AlertRepository alertRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _mapper = mapper;
        }

        public async Task<List<AlertDto>> GetAllAlertsAsync()
        {
            var alerts = await _alertRepository.GetAllAlertsAsync();
            return _mapper.Map<List<AlertDto>>(alerts);
        }

        public async Task<List<AlertDto>> GetAlertsByUserIdAsync(int userId)
        {
            var alerts = await _alertRepository.GetAlertsByUserIdAsync(userId);
            return _mapper.Map<List<AlertDto>>(alerts);
        }

        public async Task<AlertDto> CreateAlertAsync(AlertDto alertDto)
        {
            var alert = _mapper.Map<Alert>(alertDto);
            alert.Timestamp = DateTime.UtcNow;

            await _alertRepository.AddAlertAsync(alert);

            return _mapper.Map<AlertDto>(alert);
        }

        public async Task DeleteAlertAsync(int id)
        {
            await _alertRepository.DeleteAlertAsync(id);
        }
    }
}
