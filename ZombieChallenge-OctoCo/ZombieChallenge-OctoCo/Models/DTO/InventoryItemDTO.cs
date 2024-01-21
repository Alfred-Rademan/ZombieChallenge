using System.ComponentModel.DataAnnotations;

namespace ZombieChallenge_OctoCo.Models.DTO
{
    public class InventoryItemDTO
    {
        [Required]
        public string Item { get; set; } = null!;

        [Required]
        public int Amount { get; set; }
    }
}
