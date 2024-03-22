using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class PlateSize
{
    public int Id { get; set; }

    public string PlateType { get; set; } = null!;

    public decimal? VegPlateCost { get; set; }

    public decimal? NonvegPlateCost { get; set; }

    public int? Starter { get; set; }

    public int? Curry { get; set; }

    public int? Bread { get; set; }

    public int? Rice { get; set; }

    public int? Dessert { get; set; }

    public bool? SodaorWater { get; set; }

    public decimal? BothCost { get; set; }

    public virtual ICollection<FoodTypeCount> FoodTypeCounts { get; set; } = new List<FoodTypeCount>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
