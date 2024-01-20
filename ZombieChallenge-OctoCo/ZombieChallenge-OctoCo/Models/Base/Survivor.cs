using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ZombieChallenge_OctoCo.Models.Base;

public partial class Survivor
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Age { get; set; }

    [Required]
    public string? Gender { get; set; }

    public virtual ICollection<InventoryItem>? InventoryItems { get; set; } = new List<InventoryItem>();

    public virtual ICollection<Location>? Locations { get; set; } = new List<Location>();

    public string CreationBindings = "Name,Age,Gender,Locations.Latitude,Location.Longitude,InventoryItems.Item, Inventory.Amount";
}
