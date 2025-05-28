using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Faq
{
    public string FaqId { get; set; } = null!;

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public string EmployeeId { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
