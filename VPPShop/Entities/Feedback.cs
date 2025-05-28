using System;
using System.Collections.Generic;

namespace VPPShop.Entities;

public partial class Feedback
{
    public string FeedbackId { get; set; } = null!;

    public string TopicId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly FeedbackDate { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool NeedReply { get; set; }

    public string? Reply { get; set; }

    public DateOnly? ReplyDate { get; set; }

    public virtual Topic Topic { get; set; } = null!;
}
