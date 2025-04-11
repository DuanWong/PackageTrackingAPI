using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class TrackingEventService
    {
        private readonly TrackingEventRepository _trackingEventRepository;
        private readonly IMapper _mapper;

        public TrackingEventService(TrackingEventRepository trackingEventRepository, IMapper mapper)
        {
            _trackingEventRepository = trackingEventRepository;
            _mapper = mapper;
        }

        public async Task<TrackingEventDto> GetTrackingEventByIdAsync(int id)
        {
            var trackingEvent = await _trackingEventRepository.GetByIdAsync(id);
            return _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        public async Task<List<TrackingEventDto>> GetTrackingEventsByPackageIdAsync(int packageId)
        {
            var trackingEvents = await _trackingEventRepository.GetByPackageIdAsync(packageId);
            return _mapper.Map<List<TrackingEventDto>>(trackingEvents);
        }

        public async Task<TrackingEventDto> GetLatestTrackingEventAsync(int packageId)
        {
            var trackingEvent = await _trackingEventRepository.GetLatestEventAsync(packageId);
            return _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        public async Task<TrackingEventDto> CreateTrackingEventAsync(TrackingEventDto trackingEventDto)
        {
            var trackingEvent = _mapper.Map<TrackingEvent>(trackingEventDto);
            await _trackingEventRepository.AddAsync(trackingEvent);
            return _mapper.Map<TrackingEventDto>(trackingEvent);
        }

        public async Task UpdateTrackingEventAsync(int id, TrackingEventDto trackingEventDto)
        {
            var trackingEvent = await _trackingEventRepository.GetByIdAsync(id);
            if (trackingEvent != null)
            {
                _mapper.Map(trackingEventDto, trackingEvent);
                await _trackingEventRepository.UpdateAsync(trackingEvent);
            }
        }

        public async Task DeleteTrackingEventAsync(int id)
        {
            await _trackingEventRepository.DeleteAsync(id);
        }
    }
}