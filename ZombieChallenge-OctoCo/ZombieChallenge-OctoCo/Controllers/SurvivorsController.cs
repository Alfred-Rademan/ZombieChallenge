using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZombieChallenge_OctoCo.Models;

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

          //remove location and inventory items from the request
            survivor.Locations = null;
            survivor.InventoryItems = null;
            _context.Survivors.Add(survivor);
            await _context.SaveChangesAsync();

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
