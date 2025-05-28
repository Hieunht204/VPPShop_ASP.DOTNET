using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Topic
{
    public string TopicId { get; set; } = null!;

    public string? TopicName { get; set; }

    public string? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
