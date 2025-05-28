using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? AliasName { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
