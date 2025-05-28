using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Webpage
{
    public string PageId { get; set; } = null!;

    public string PageName { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
