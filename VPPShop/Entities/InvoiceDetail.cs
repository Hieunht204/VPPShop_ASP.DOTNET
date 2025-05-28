using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class InvoiceDetail
{
    public string InvoiceDetailId { get; set; } = null!;

    public string? InvoiceId { get; set; }

    public string? ProductId { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double Discount { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Product? Product { get; set; }
}
