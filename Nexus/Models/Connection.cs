using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Connection
{
    public string ConnectionId { get; set; } = null!;

    public string? AccountId { get; set; }

    public int? PackageId { get; set; }

    public string? Status { get; set; }

    public DateOnly? InstallationDate { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public string? OrderId { get; set; }

    public virtual Customer? Account { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ServicePackage? Package { get; set; }
}
