using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Department
{
    public string DepartmentId { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
