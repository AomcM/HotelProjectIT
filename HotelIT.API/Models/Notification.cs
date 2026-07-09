using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual Users User { get; set; } = null!;
}
