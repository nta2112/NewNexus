using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
