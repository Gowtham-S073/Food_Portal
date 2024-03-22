using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class StdFoodOrderDetail
{
    public long Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductsId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual StdProduct? Products { get; set; }
}
