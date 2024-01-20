using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZombieChallenge_OctoCo.Models;

public partial class Location
{
    public int? Id { get; set; }

    [Required]
    public decimal Latitude { get; set; }

    [Required]
    public decimal Longitude { get; set; }

    [Required]
    public int SurvivorsId { get; set; }

    public virtual Survivor? Survivors { get; set; }
}
