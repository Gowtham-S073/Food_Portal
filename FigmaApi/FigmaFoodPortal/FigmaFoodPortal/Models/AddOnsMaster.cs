using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AddOnsMaster
{
    public int Id { get; set; }

    public string AddOnsName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }

    public string? AddOnsImage { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<AddOnsDetail> AddOnsDetails { get; set; } = new List<AddOnsDetail>();
}
