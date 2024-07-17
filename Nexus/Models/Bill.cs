using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public string OrderId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
