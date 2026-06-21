using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class LabTest
{
    public int TestId { get; set; }

    public int? BatchId { get; set; }
    public int? MatBatchId { get; set; }
    public string TestType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? AssignedTo { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? ResultsText { get; set; }

    public string? OverallResult { get; set; }

    public int? DecisionBy { get; set; }

    public DateTime? DecisionAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Priority { get; set; } = null!;
    public string? Comment { get; set; }
    public string? ControlledParameters { get; set; }
    [JsonIgnore]
    public virtual User? AssignedToNavigation { get; set; }
    [JsonIgnore]
    public virtual ProductionBatch? Batch { get; set; } = null!;
    [JsonIgnore]
    public virtual MaterialBatch? MatBatch { get; set; }
    [JsonIgnore]
    public virtual User? DecisionByNavigation { get; set; }
}
