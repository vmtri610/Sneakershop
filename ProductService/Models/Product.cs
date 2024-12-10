using System;
using System.Collections.Generic;

namespace ProductService.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public double Price { get; set; }

    public string ImageUrl { get; set; } = null!;
}
