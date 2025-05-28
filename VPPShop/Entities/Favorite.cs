using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Favorite
{
    public string FavoriteId { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? CustomerId { get; set; }

    public DateTime? SelectedDate { get; set; }

    public string? Description { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
