using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Permission
{
    public string PermissionId { get; set; } = null!;

    public string? DepartmentId { get; set; }

    public string? PageId { get; set; }

    public bool CanAdd { get; set; }

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public bool CanView { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Webpage? Page { get; set; }
}
