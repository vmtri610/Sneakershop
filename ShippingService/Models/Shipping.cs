using System;
using System.Collections.Generic;

namespace ShippingService.Models;

public partial class Shipping
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public string Status { get; set; } = null!;
}
