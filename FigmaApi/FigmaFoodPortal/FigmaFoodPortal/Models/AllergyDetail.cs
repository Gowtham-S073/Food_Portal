using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AllergyDetail
{
    public int Id { get; set; }

    public int? OrdersId { get; set; }

    public int? AllergyId { get; set; }

    public virtual AllergyMaster? Allergy { get; set; }

    public virtual Order? Orders { get; set; }
}
