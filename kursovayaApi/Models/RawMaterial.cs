using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class RawMaterial
{
    public int MaterialId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int UomId { get; set; }

    public int? ShelfLifeDays { get; set; }

    public string Status { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<MaterialBatch>? MaterialBatches { get; set; } = new List<MaterialBatch>();
    [JsonIgnore]
    public virtual ICollection<RecipeComponent>? RecipeComponents { get; set; } = new List<RecipeComponent>();
    [JsonIgnore]
    public virtual UnitsOfMeasure? Uom { get; set; } = null!;
}
