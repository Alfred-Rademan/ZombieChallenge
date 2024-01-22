using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.SurvivorService
{
    public interface ISurvivorService
    {
        public Task<Survivor?> RegisterSurvivor(SurvivorDTO survivorDTO);
        public Task<Survivor?> GetSurvivor(int id);
        public Task<List<Survivor>?> GetSurvivors();
        public Task<Survivor?> InfectSurvivor(int id);
    }
}
