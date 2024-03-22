using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class DeliveryOption
{
    public int Id { get; set; }

    public string DeliveryOption1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
