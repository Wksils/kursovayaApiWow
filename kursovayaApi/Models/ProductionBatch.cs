using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class ProductionBatch
{
    public int BatchId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public int ProductId { get; set; }

    public int RecipeId { get; set; }

    public int CardId { get; set; }

    public decimal PlannedQty { get; set; }

    public decimal? ActualQty { get; set; }

    public int UomId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? StartedBy { get; set; }

    public int? CompletedBy { get; set; }

    public string? QaDecision { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<BatchStepExecution>? BatchStepExecutions { get; set; } = new List<BatchStepExecution>();
    [JsonIgnore]
    public virtual TechCard? Card { get; set; } = null!;
    [JsonIgnore]
    public virtual User? CompletedByNavigation { get; set; }
    [JsonIgnore]
    public virtual User? CreatedByNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<LabTest>? LabTests { get; set; } = new List<LabTest>();
    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
    [JsonIgnore]
    public virtual Recipe? Recipe { get; set; } = null!;
    [JsonIgnore]
    public virtual User? StartedByNavigation { get; set; }
    [JsonIgnore]
    public virtual UnitsOfMeasure? Uom { get; set; } = null!;
}
