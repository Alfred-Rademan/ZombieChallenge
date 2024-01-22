using AutoMapper;
using Microsoft.EntityFrameworkCore;
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


        public async Task<Location?> RegisterLocation(LocationDTO locationDTO, int survivorID)
        {
            try
            {
                Location location = _mapper.Map<Location>(locationDTO);
                location.SurvivorsId = survivorID;
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return location;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Location?> UpdateLocation(LocationDTO locationDTO, int survivorID)
        {
            try
            {
                Location? location = await _context.Locations.FirstOrDefaultAsync(l => l.SurvivorsId == survivorID);
                if (location == null)
                {
                    return null;
                }
                location.Latitude = locationDTO.Latitude;
                location.Longitude = locationDTO.Longitude;
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
