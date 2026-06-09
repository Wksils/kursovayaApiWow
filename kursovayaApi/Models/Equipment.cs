using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class Equipment
{
    public int EquipmentId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Status { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<TechStep>? TechSteps { get; set; } = new List<TechStep>();
}
