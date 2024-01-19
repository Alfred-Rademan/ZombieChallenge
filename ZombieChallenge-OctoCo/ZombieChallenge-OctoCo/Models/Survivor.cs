using System;
using System.Collections.Generic;

namespace ZombieChallenge_OctoCo.Models;

public partial class Survivor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int LocationId { get; set; }

    public int InventoryId { get; set; }

    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
