using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class EmailVerification
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
