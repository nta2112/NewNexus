using System;
using System.Collections.Generic;

namespace Nexus.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? BillId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public virtual Bill? Bill { get; set; }
}
