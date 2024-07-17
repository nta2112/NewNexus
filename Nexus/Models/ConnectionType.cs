using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class ConnectionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ServicePackage> ServicePackages { get; set; } = new List<ServicePackage>();
}
