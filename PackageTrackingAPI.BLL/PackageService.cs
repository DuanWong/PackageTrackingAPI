using AutoMapper;
using PackageTrackingAPI.DAL;
using PackageTrackingAPI.DTOs;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.BLL
{
    public class PackageService
    {
        private readonly PackageRepository _packageRepository;
        private readonly IMapper _mapper;

        public PackageService(PackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        public async Task<PackageDto> GetPackageByIdAsync(int id)
        {
            var package = await _packageRepository.GetByIdAsync(id);
            return _mapper.Map<PackageDto>(package);
        }

        public async Task<PackageDto> GetPackageByTrackingNumberAsync(string trackingNumber)
        {
            var package = await _packageRepository.GetByTrackingNumberAsync(trackingNumber);
            return _mapper.Map<PackageDto>(package);
        }

        public async Task<PackageDto> CreatePackageAsync(PackageDto packageDto)
        {
            var package = _mapper.Map<Package>(packageDto);
            await _packageRepository.AddAsync(package);
            return _mapper.Map<PackageDto>(package);
        }

        public async Task UpdatePackageAsync(int id, PackageDto packageDto)
        {
            var package = await _packageRepository.GetByIdAsync(id);
            if (package != null)
            {
                _mapper.Map(packageDto, package);
                await _packageRepository.UpdateAsync(package);
            }
        }

        public async Task DeletePackageAsync(int id)
        {
            await _packageRepository.DeleteAsync(id);
        }
    }
}