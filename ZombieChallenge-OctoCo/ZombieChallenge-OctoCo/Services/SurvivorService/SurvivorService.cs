using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                Survivor? survivor = await _context.Survivors.Include(s => s.InventoryItems).Include(s => s.Locations).FirstOrDefaultAsync(s => s.Id == id);
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
               // Get survivors from database in a list in alphabetical order
               List<Survivor>? survivors = await _context.Survivors.ToListAsync();
               survivors.Sort((x, y) => string.Compare(x.Name, y.Name));
               return survivors;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Survivor?> InfectSurvivor(int id)
        {
            try
            {
                Survivor? survivor = await _context.Survivors.FindAsync(id);
                if (survivor == null || survivor.Infected == true)
                {
                    return null;
                }
                survivor.Infected = true;
                await _context.SaveChangesAsync();
                return survivor;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckGender(SurvivorDTO survivorDTO)
        {
            List<string> validGenders = new List<string> { "male", "female", "other" };
            return validGenders.Contains(survivorDTO.Gender);
        }

    }
}
