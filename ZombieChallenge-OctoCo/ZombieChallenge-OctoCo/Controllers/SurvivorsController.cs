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
        /*
         EXPLINATIONS:
        - The controller is responsible for receiving the requests and sending the responses. This is the only class that should be aware of any HTTP. And thus it is where 
          the requests and responses are handled.
        - The controller should not be aware of the business logic, it should only be aware of the services that are responsible for the business logic.
        - The controller should not be aware of the database, it should only be aware of the services that are responsible for the database.
        - The controller only takes in DTOs and returns the full objects. This is to make the schema more sensible to the user.
        - Interfaces are used to make the code more testable and to make it easier to change the implementation of the services if needed.

        THOUGHTS:
        - All 3 services are injected into this controller and are used to handle the requests and responses. There are 2 alternatives to this. One is to inject the location and 
          inventory services into the survivor service and use that service to handle all things pertaining to the survivor, location and inventory.
          I decided against this because I believe that the services should be as small as possible and only handle one thing. This makes the code more readable and easier to maintain.
          The other feasable option would have been to create seperate controllers for each service. I believe that this is the best option in general, but for this project I decided
          against it because all actions to be performed are still spesific to the survivor. If there were more actions to be performed on the location and inventory outside of the survivor, I would have
          seperated the services into their own controllers.
        - Usually I would not return full objects for risk of exposing the underlying database structure but since we are in a zombie apocalypse and the world is ending, I decided
          to return the full objects to save some time.
        - I decided to use DTO's instead of just using bind because it shows up better in the OpenAPI documentation and it is easier to read and enter when using the try me feature.
         */
        public SurvivorsController(ISurvivorService survivorService, ILocationService locationService, IInventoryService inventoryService)
        {
            _survivorService = survivorService;
            _locationService = locationService;
            _inventoryService = inventoryService;
        }

        // Get all survivors in alphabetical order
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

        // Get a specific survivor
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

        // Create a new survivor
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

        // Infect a survivor
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

        // Update a survivor's location
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
