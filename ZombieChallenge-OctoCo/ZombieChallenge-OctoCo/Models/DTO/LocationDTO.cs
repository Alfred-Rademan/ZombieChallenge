using System.ComponentModel.DataAnnotations;

namespace ZombieChallenge_OctoCo.Models.DTO
{
    public class LocationDTO
    {
        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }
    }
}
