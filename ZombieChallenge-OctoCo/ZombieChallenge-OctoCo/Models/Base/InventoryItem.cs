﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZombieChallenge_OctoCo.Models.Base;

public partial class InventoryItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Item { get; set; } = null!;

    [Required]
    public int Amount { get; set; }

    [Required]
    public int SurvivorsId { get; set; }

    public virtual Survivor? Survivors { get; set; }
}
