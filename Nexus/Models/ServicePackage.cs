using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class ServicePackage
{
    public int PackageId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int? ConnectionTypeId { get; set; }

    public virtual ConnectionType? ConnectionType { get; set; }

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
