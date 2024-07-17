using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int VendorId { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Vendor Vendor { get; set; } = null!;
}
