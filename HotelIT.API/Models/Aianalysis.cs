using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Aianalysis
{
    public int AnalysisId { get; set; }

    public int TicketId { get; set; }

    public string Category { get; set; } = null!;

    public string SuggestedPriority { get; set; } = null!;

    public string SuggestedSolution { get; set; } = null!;
    [JsonIgnore]
    public virtual Tickets Ticket { get; set; } = null!;
}
