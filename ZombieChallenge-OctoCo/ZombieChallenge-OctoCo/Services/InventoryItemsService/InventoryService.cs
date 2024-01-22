using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZombieChallenge_OctoCo.Models;
using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.InventoryItemsService
{
    public class InventoryService : IInventoryService
    {
        private readonly ZombieSurvivorsContext _context;
        private readonly IMapper _mapper;
        public InventoryService(ZombieSurvivorsContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InventoryItem>?> RegisterItems(List<InventoryItemDTO> inventoryItemDTOs, int survivorID)
        {
            try
            {
                List<InventoryItem> inventoryItems = _mapper.Map<List<InventoryItem>>(inventoryItemDTOs);
                foreach (var item in inventoryItems)
                {
                    item.SurvivorsId = survivorID;
                    _context.InventoryItems.Add(item);
                    await _context.SaveChangesAsync();

                    //Reload the context to get the new id for tracking of entity in memory
                    _context.Entry(item).State = EntityState.Detached;


                }
                

                return inventoryItems;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
