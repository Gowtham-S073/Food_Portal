using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class TimeSlot
{
    public int Id { get; set; }

    public string AddTimeSlot { get; set; } = null!;

    public TimeSpan? StartTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
