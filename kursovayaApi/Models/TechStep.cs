using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class TechStep
{
    public int StepId { get; set; }

    public int CardId { get; set; }

    public int StepNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? EquipmentId { get; set; }

    public int? DurationMin { get; set; }

    public bool IsCritical { get; set; }

    public string? ParamsNote { get; set; }
    [JsonIgnore]
    public virtual ICollection<BatchStepExecution>? BatchStepExecutions { get; set; } = new List<BatchStepExecution>();
    [JsonIgnore]
    public virtual TechCard? Card { get; set; } = null!;
    [JsonIgnore]
    public virtual Equipment? Equipment { get; set; }
}
