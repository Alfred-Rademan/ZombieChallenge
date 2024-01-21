using AutoMapper;
using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryItemDTO, InventoryItem>();
            CreateMap<LocationDTO, Location>();
            CreateMap<SurvivorDTO, Survivor>();
        }
    }
}
