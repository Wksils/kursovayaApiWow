using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int ProductId { get; set; }

    public string Version { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Notes { get; set; }
    [JsonIgnore]
    public virtual User? ApprovedByNavigation { get; set; }
    [JsonIgnore]
    public virtual User? CreatedByNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ProductionBatch>? ProductionBatches { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<RecipeComponent>? RecipeComponents { get; set; } = new List<RecipeComponent>();
}
