using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class DrinksMaster
{
    public int Id { get; set; }

    public string? DrinkName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
