using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AdditionalProductsDetail
{
    public long Id { get; set; }

    public int? AdditionalProductsId { get; set; }

    public int? OrderId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Cost { get; set; }

    public virtual AdditionalProduct? AdditionalProducts { get; set; }

    public virtual Order? Order { get; set; }
}
