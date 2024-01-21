using System.ComponentModel.DataAnnotations;
using ZombieChallenge_OctoCo.Models.Base;

namespace ZombieChallenge_OctoCo.Models.DTO
{
    public class SurvivorDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        public virtual ICollection<InventoryItemDTO>? InventoryItems { get; set; } = new List<InventoryItemDTO>();

        public virtual ICollection<LocationDTO>? Locations { get; set; } = new List<LocationDTO>();
    }
}
