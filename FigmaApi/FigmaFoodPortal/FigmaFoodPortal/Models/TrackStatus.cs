using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class TrackStatus
{
    public int? OrderId { get; set; }

    public string? Status { get; set; }

    public int Id { get; set; }

    public string? Tid { get; set; }

    public virtual Order? Order { get; set; }
}
