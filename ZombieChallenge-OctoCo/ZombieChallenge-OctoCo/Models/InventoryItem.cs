using System;
using System.Collections.Generic;

namespace ZombieChallenge_OctoCo.Models;

public partial class InventoryItem
{
    public int Id { get; set; }

    public string Item { get; set; } = null!;

    public int Amount { get; set; }

    public int SurvivorsId { get; set; }

    public virtual Survivor Survivors { get; set; } = null!;
}
