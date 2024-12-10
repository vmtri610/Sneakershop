using System;
using System.Collections.Generic;

namespace OrderService.Models;

public partial class OrderDetail
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ProdId { get; set; }

    public byte[] Quantity { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
