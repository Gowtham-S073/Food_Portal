using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class StdFoodCategoryMaster
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<StdProduct> StdProducts { get; set; } = new List<StdProduct>();
}
