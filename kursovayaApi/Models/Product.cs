using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ReleaseForm { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    [JsonIgnore]
    public virtual ICollection<TechCard> TechCards { get; set; } = new List<TechCard>();
}
