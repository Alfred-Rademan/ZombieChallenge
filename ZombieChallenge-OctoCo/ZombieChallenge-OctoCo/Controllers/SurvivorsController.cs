using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Models.Base;

namespace ZombieChallenge_OctoCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurvivorsController : ControllerBase
    {
        private readonly ZombieSurvivorsContext _context;

        public SurvivorsController(ZombieSurvivorsContext context)
        {
            _context = context;
        }

        // GET: api/Survivors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survivor>>> GetSurvivors()
        {
          if (_context.Survivors == null)
          {
              return NotFound();
          }
            return await _context.Survivors.ToListAsync();
        }

        // GET: api/Survivors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survivor>> GetSurvivor(int id)
        {
          if (_context.Survivors == null)
          {
              return NotFound();
          }
            var survivor = await _context.Survivors.FindAsync(id);

            if (survivor == null)
            {
                return NotFound();
            }

            return survivor;
        }

        // PUT: api/Survivors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvivor(int id, Survivor survivor)
        {
            if (id != survivor.Id)
            {
                return BadRequest();
            }

            _context.Entry(survivor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurvivorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Survivors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Survivor>> PostSurvivor(Survivor survivor)
        {
          if (_context.Survivors == null)
          {
              return Problem("Entity set 'ZombieSurvivorsContext.Survivors'  is null.");
          }
            //split the object into three objects, survivor, location and inventory items
            Survivor insertSurvivor = survivor;
            Location insertLocation = survivor.Locations.FirstOrDefault();
            List<InventoryItem> insertInventoryItems = survivor.InventoryItems.ToList();
          
          //remove location and inventory items from the request
            insertSurvivor.Locations = null;
            insertSurvivor.InventoryItems = null;

            //add the survivor to the database
            _context.Survivors.Add(insertSurvivor);
            await _context.SaveChangesAsync();

            //add the location to the database
            if (insertLocation != null)
            {
                insertLocation.SurvivorsId = insertSurvivor.Id;
                _context.Locations.Add(insertLocation);
            }
            else
            {
                return BadRequest("Survivor does not have a location");
            }
            
            //add the inventory items to the database
            foreach (var item in insertInventoryItems)
            {
                //change the survivor id to new id of the saved survivor
                if (item.SurvivorsId != null)
                {
                    item.SurvivorsId = insertSurvivor.Id;
                    _context.InventoryItems.Add(item);
                }
                else
                {
                    return BadRequest("Inventory item does not have a survivor id");
                }
                
            }

            return CreatedAtAction("GetSurvivor", new { id = survivor.Id }, survivor);
        }

        // DELETE: api/Survivors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvivor(int id)
        {
            if (_context.Survivors == null)
            {
                return NotFound();
            }
            var survivor = await _context.Survivors.FindAsync(id);
            if (survivor == null)
            {
                return NotFound();
            }

            _context.Survivors.Remove(survivor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurvivorExists(int id)
        {
            return (_context.Survivors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
