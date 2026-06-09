using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class BatchStepExecution
{
    public int ExecutionId { get; set; }

    public int BatchId { get; set; }

    public int StepId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? StartedBy { get; set; }

    public int? CompletedBy { get; set; }

    public string? ActualParams { get; set; }

    public string? Notes { get; set; }

    [JsonIgnore]
    public virtual ProductionBatch? Batch { get; set; } = null!;
    [JsonIgnore]
    public virtual User? CompletedByNavigation { get; set; }
    [JsonIgnore]
    public virtual User? StartedByNavigation { get; set; }
    [JsonIgnore]
    public virtual TechStep? Step { get; set; } = null!;
}
