using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class RetailShop
{
    public int ShopId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
