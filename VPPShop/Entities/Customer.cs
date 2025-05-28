using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public bool Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Image { get; set; }

    public bool Validity { get; set; }

    public int Role { get; set; }

    public virtual ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
