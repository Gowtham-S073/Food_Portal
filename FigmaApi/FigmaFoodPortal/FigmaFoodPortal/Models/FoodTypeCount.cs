using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class FoodTypeCount
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? FoodTypeCount1 { get; set; }

    public bool? IsVeg { get; set; }

    public int? PlateSizeId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual PlateSize? PlateSize { get; set; }
}
