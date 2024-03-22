using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class AllergyMaster
{
    public int Id { get; set; }

    public string? AllergyName { get; set; }

    public virtual ICollection<AllergyDetail> AllergyDetails { get; set; } = new List<AllergyDetail>();
}
