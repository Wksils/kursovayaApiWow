using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class RecipeComponent
{
    public int ComponentId { get; set; }

    public int RecipeId { get; set; }

    public int MaterialId { get; set; }

    public decimal Quantity { get; set; }

    public int UomId { get; set; }

    public decimal Percentage { get; set; }

    public bool IsCritical { get; set; }
    [JsonIgnore]
    public virtual RawMaterial? Material { get; set; } = null!;
    [JsonIgnore]
    public virtual Recipe? Recipe { get; set; } = null!;
    [JsonIgnore]
    public virtual UnitsOfMeasure? Uom { get; set; } = null!;
}
