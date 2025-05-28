using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Assignment
{
    public string AssignmentId { get; set; } = null!;

    public string EmployeeId { get; set; } = null!;

    public string DepartmentId { get; set; } = null!;

    public DateTime? AssignDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
