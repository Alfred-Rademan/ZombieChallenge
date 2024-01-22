using Microsoft.AspNetCore.Mvc;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;
using ZombieChallenge_OctoCo.Services.InventoryItemsService;
using ZombieChallenge_OctoCo.Services.LocationService;
using ZombieChallenge_OctoCo.Services.SurvivorService;

namespace ZombieChallenge_OctoCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurvivorsController : ControllerBase
    {
        private readonly ISurvivorService _survivorService;
        private readonly ILocationService _locationService;
        private readonly IInventoryService _inventoryService;
        public SurvivorsController(ZombieSurvivorsContext context, ISurvivorService survivorService, ILocationService locationService, IInventoryService inventoryService)
        {
            _survivorService = survivorService;
            _locationService = locationService;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survivor>>> GetSurvivors()
        {
          List<Survivor>? survivors = await _survivorService.GetSurvivors();
            if (survivors == null)
            {
                return NotFound();
            }
            return survivors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Survivor>> GetSurvivor(int id)
        {
          Survivor? survivor = await _survivorService.GetSurvivor(id);
            if (survivor == null)
            {
                return NotFound();
            }

            return survivor;
        }


        [HttpPost]
        public async Task<ActionResult<Survivor>> AddSurvivor(SurvivorDTO survivorDTO)
        {
            if (!_survivorService.CheckGender(survivorDTO))
            {
                return BadRequest("Gender must be one of the following [male, female, other]");
            }
          
            Survivor? survivor = await _survivorService.RegisterSurvivor(survivorDTO);
            
            if (survivor == null)
            {
                return BadRequest("Survivor could not be created");
            }
            
            int survivorID = survivor.Id;

            LocationDTO? locationDTO = survivorDTO.LocationsDTO;
            Location? insertLocation = await _locationService.RegisterLocation(locationDTO, survivorID);

            if (insertLocation == null)
            {
                return BadRequest("Location could not be created");
            }
            List<InventoryItemDTO>? inventoryItemDTOs = survivorDTO.InventoryItemsDTO.ToList();
            List<InventoryItem>? insertInventoryItems = await _inventoryService.RegisterItems(inventoryItemDTOs, survivorID);

            if (insertInventoryItems == null)
            {
                return BadRequest("Inventory items could not be created");
            }

            return CreatedAtAction("GetSurvivor", new { id = survivor.Id }, survivor);
        }

        [HttpPut]
        [Route("Infect/{id}")]
        public async Task<ActionResult<Survivor>> InfectSurvivor(int id)
        {
            Survivor? survivor = await _survivorService.InfectSurvivor(id);
            if (survivor == null)
            {
                return BadRequest("Survivor could not be infected");
            }
            
            Survivor? infectedSurvivor = await _survivorService.GetSurvivor(id);
            if (infectedSurvivor == null)
            {
                return NotFound();
            }
            return infectedSurvivor;

        }

        [HttpPut]
        [Route("UpdateLocation/{id}")]
        public async Task<ActionResult<Survivor>> UpdateLocation(int id, LocationDTO locationDTO)
        {
            Survivor? survivor = await _survivorService.GetSurvivor(id);
            if (survivor == null)
            {
                return NotFound();
            }
            Location? location = await _locationService.UpdateLocation(locationDTO, id);
            if (location == null)
            {
                return BadRequest("Location could not be updated");
            }

            Survivor? updatedSurvivor = await _survivorService.GetSurvivor(id);
            if (updatedSurvivor == null)
            {
                return NotFound();
            }
            return updatedSurvivor;
        }

        
    }
}
