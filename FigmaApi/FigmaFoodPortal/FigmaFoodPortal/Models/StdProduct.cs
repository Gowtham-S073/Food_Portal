using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class StdProduct
{
    public int Id { get; set; }

    public int? CategoriesId { get; set; }

    public string? ProductsName { get; set; }

    public int? IsVeg { get; set; }

    public string? FoodProductImage { get; set; }

    public bool? IsVegan { get; set; }

    public bool? IsGluten { get; set; }

    public bool? IsSpicy { get; set; }

    public virtual StdFoodCategoryMaster? Categories { get; set; }

    public virtual ICollection<StdFoodOrderDetail> StdFoodOrderDetails { get; set; } = new List<StdFoodOrderDetail>();
}
