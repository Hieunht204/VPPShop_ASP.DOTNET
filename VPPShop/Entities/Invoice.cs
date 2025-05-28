using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Invoice
{
    public string InvoiceId { get; set; } = null!;

    public string CustomerId { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime? RequireDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Fullname { get; set; }

    public string Address { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string ShippingMethod { get; set; } = null!;

    public double ShippingFee { get; set; }

    public string StatusId { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? Note { get; set; }

    public string? Phonenumber { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Statuss Status { get; set; } = null!;
}
