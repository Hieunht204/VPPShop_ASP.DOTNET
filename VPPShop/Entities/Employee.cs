using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PassWord { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Faq> Faqs { get; set; } = new List<Faq>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
