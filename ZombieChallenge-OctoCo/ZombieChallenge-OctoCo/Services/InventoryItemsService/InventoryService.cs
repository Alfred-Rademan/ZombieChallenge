using AutoMapper;
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

        public async Task<List<InventoryItem>?> registerItems(List<InventoryItemDTO> inventoryItemDTOs)
        {
            try
            {
                List<InventoryItem> inventoryItems = _mapper.Map<List<InventoryItem>>(inventoryItemDTOs);
                _context.InventoryItems.AddRange(inventoryItems);
                await _context.SaveChangesAsync();
                return inventoryItems;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
