using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AdditionalCategoryMaster
{
    public int Id { get; set; }

    public string AdditionalCategory { get; set; } = null!;

    public virtual ICollection<AdditionalProduct> AdditionalProducts { get; set; } = new List<AdditionalProduct>();
}
