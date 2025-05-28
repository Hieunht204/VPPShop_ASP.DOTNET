using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? AliasName { get; set; }

    public string CategoryId { get; set; } = null!;

    public string? UnitDescription { get; set; }

    public double? UnitPrice { get; set; }

    public string? Image { get; set; }

    public DateTime ManufactureDate { get; set; }

    public double Discount { get; set; }

    public int ViewCount { get; set; }

    public string? Description { get; set; }

    public string SupplierId { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
