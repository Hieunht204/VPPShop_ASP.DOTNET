using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Statuss
{
    public string StatusId { get; set; } = null!;

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
