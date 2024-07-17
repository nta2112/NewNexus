using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int PackageId { get; set; }

    public int ShopId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public virtual Customer Account { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ServicePackage Package { get; set; } = null!;

    public virtual RetailShop Shop { get; set; } = null!;
}
