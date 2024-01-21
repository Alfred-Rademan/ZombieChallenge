using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.SurvivorService
{
    public class SurvivorService : ISurvivorService
    {
        private readonly ZombieSurvivorsContext _context;
        private readonly IMapper _mapper;
        public SurvivorService(ZombieSurvivorsContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<Survivor?> RegisterSurvivor(SurvivorDTO survivorDTO)
        {
            try
            {
                Survivor survivor = _mapper.Map<Survivor>(survivorDTO);
                //split the object into three objects, survivor, location and inventory items
                survivor.Locations = null;
                survivor.InventoryItems = null;

                //add the survivor to the database
                _context.Survivors.Add(survivor);
                await _context.SaveChangesAsync();

                return survivor;
            }
            catch (Exception e)
            {
                return null;
            }
            

        }

        public async Task<Survivor?> GetSurvivor(int id)
        {
            try
            {
                Survivor? survivor = await _context.Survivors.FindAsync(id);
                return survivor;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Survivor>?> GetSurvivors()
        {
            try
            {
                List<Survivor> survivors = await _context.Survivors.ToListAsync();
                return survivors;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
