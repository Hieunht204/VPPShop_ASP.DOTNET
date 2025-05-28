using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Friend
{
    public string FriendId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public string ProductId { get; set; } = null!;

    public string? Fullname { get; set; }

    public string Email { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public string? Note { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product Product { get; set; } = null!;
}
