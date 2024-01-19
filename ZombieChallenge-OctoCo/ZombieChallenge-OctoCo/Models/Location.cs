using System;
using System.Collections.Generic;

namespace ZombieChallenge_OctoCo.Models;

public partial class Location
{
    public int Id { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int SurvivorsId { get; set; }

    public virtual Survivor Survivors { get; set; } = null!;
}
