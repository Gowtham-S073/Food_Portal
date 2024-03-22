using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class User
{
    public string Name { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Role { get; set; }

    public byte[]? HashKey { get; set; }

    public byte[]? Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
