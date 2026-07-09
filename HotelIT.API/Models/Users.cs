using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Users
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int RoleId { get; set; }

    public int DepartmentId { get; set; }

    [JsonIgnore]
    public virtual Departments Department { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();

    [JsonIgnore]
    public virtual Roles Role { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
}
