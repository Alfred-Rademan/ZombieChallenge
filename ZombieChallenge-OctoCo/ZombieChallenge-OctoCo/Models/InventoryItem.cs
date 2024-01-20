using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZombieChallenge_OctoCo.Models;

public partial class InventoryItem
{
    
    public int Id { get; set; }

    [Required]
    public string Item { get; set; } = null!;

    [Required]
    public int Amount { get; set; }

    [Required]
    public int? SurvivorsId { get; set; }

    public virtual Survivor? Survivors { get; set; }
}
