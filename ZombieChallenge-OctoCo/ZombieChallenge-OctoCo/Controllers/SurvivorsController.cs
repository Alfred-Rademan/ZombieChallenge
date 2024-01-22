using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ZombieSurvivorsContext _context;
        private readonly ISurvivorService _survivorService;
        private readonly ILocationService _locationService;
        private readonly IInventoryService _inventoryService;
        public SurvivorsController(ZombieSurvivorsContext context, ISurvivorService survivorService, ILocationService locationService, IInventoryService inventoryService)
        {
            _context = context;
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
            Survivor? survivorReturn = await _survivorService.GetSurvivor(survivorID);
            if (survivorReturn == null)
            {
                return BadRequest("Survivor could not be created");
            }
            return survivorReturn;
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
            return survivor;
        }

        [HttpPut]
        [Route("UpdateLocation/{id}")]
        public async Task<ActionResult<Location>> UpdateLocation(int id, LocationDTO locationDTO)
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
            return location;
        }

        
    }
}
