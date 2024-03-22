using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class GroupSize
{
    public int Id { get; set; }

    public string GroupType { get; set; } = null!;

    public int MinValue { get; set; }

    public int MaxValue { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
