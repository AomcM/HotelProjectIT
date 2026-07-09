using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Assets
{
    public int AssetId { get; set; }

    public string AssetName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public DateOnly PurchaseDate { get; set; }

    public string Status { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int DepartmentId { get; set; }

    [JsonIgnore]
    public virtual Departments Department { get; set; } = null!;
}
