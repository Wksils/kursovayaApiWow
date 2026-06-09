using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class UnitsOfMeasure
{
    public int UomId { get; set; }

    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<MaterialBatch>? MaterialBatches { get; set; } = new List<MaterialBatch>();
    [JsonIgnore]
    public virtual ICollection<ProductionBatch>? ProductionBatches { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<RawMaterial>? RawMaterials { get; set; } = new List<RawMaterial>();
    [JsonIgnore]
    public virtual ICollection<RecipeComponent>? RecipeComponents { get; set; } = new List<RecipeComponent>();
}
