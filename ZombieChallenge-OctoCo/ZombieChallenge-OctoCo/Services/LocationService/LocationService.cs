using AutoMapper;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ZombieSurvivorsContext _context;
        private readonly IMapper _mapper;  
        public LocationService(ZombieSurvivorsContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Location?> RegisterLocation(LocationDTO locationDTO)
        {
            try
            {
                Location location = _mapper.Map<Location>(locationDTO);
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return location;
            }
            catch (Exception e)
            {
                return null;
            }
        }

      
    }
}
