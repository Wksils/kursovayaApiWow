using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class MaterialBatch
{
    public int BatchId { get; set; }

    public int MaterialId { get; set; }

    public string BatchNumber { get; set; } = null!;

    public string? Supplier { get; set; }

    public decimal Quantity { get; set; }

    public int UomId { get; set; }

    public DateOnly? ManufactureDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public DateTime ReceivedAt { get; set; }
    public string? StorageLocation { get; set; }
    public string? QaDecision { get; set; }
    public int? DecisionBy { get; set; }
    public DateTime? DecisionAt { get; set; }
    public string? DecisionComment { get; set; }
    public string? DecisionReason { get; set; }

    public string Status { get; set; } = null!;
    [JsonIgnore]
    public virtual RawMaterial? Material { get; set; } = null!;
    [JsonIgnore]
    public virtual UnitsOfMeasure? Uom { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();
}
