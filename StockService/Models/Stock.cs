using System;
using System.Collections.Generic;

namespace StockService.Models;

public partial class Stock
{
    public long Id { get; set; }

    public long ProdId { get; set; }

    public byte[] Quantity { get; set; } = null!;
}
