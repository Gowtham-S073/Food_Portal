using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AdditionalProduct
{
    public int Id { get; set; }

    public int? AdditionalCategoryId { get; set; }

    public string AdditionalProductsName { get; set; } = null!;

    public bool? IsVeg { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? AdditionalProductsImages { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual AdditionalCategoryMaster? AdditionalCategory { get; set; }

    public virtual ICollection<AdditionalProductsDetail> AdditionalProductsDetails { get; set; } = new List<AdditionalProductsDetail>();
}
