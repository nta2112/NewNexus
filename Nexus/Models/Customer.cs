using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Customer
{
    public string AccountId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
