﻿using ZombieChallenge_OctoCo.Models.Base;
using ZombieChallenge_OctoCo.Models.DTO;

namespace ZombieChallenge_OctoCo.Services.LocationService
{
    public interface ILocationService
    {
        public Task<Location?> RegisterLocation(LocationDTO locationDTO);
    }
}
