using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Tickets
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public int TechnicianId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Aianalysis> Aianalysis { get; set; } = new List<Aianalysis>();

    [JsonIgnore]
    public virtual Users User { get; set; } = null!;
}
