﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieChallenge_OctoCo.Models.Base;

public partial class Survivor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Age { get; set; }

    [Required]
    public string? Gender { get; set; }

    public bool Infected { get; set; } = false;
    public virtual ICollection<InventoryItem>? InventoryItems { get; set; } = new List<InventoryItem>();

    public virtual ICollection<Location>? Locations { get; set; } = new List<Location>();

}
