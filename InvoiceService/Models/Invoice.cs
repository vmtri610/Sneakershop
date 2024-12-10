using System;
using System.Collections.Generic;

namespace InvoiceService.Models;

public partial class Invoice
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public string CreatedAt { get; set; } = null!;
}
