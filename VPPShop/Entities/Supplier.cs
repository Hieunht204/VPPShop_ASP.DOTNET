﻿using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
