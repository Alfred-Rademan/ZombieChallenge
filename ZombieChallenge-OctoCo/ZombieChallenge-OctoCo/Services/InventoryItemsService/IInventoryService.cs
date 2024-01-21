﻿using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.InventoryItemsService
{
    public interface IInventoryService
    {
        public Task<List<InventoryItem>?> registerItems(List<InventoryItemDTO> inventoryItemDTOs);
    }
}
