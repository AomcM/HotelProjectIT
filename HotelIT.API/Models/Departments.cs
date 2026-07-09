using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelIT.API.Models;

public partial class Departments
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Assets> Assets { get; set; } = new List<Assets>();

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
