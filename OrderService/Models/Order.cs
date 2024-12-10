using System;
using System.Collections.Generic;

namespace OrderService.Models;

public partial class Order
{
    public long Id { get; set; }

    public double Total { get; set; }

    public string CreatedAt { get; set; } = null!;

    public long UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
