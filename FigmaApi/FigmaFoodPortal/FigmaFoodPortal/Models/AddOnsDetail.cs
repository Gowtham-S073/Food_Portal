using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AddOnsDetail
{
    public long Id { get; set; }

    public int? AddOnsId { get; set; }

    public int? OrderId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Cost { get; set; }

    public virtual AddOnsMaster? AddOns { get; set; }

    public virtual Order? Order { get; set; }
}
