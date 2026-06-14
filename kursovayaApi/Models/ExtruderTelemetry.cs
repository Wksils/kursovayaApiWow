using System.Text.Json.Serialization;

namespace kursovayaApi.Models
{
    public partial class ExtruderTelemetry
    {
        public long TelemetryId { get; set; }
        public int ExecutionId { get; set; }
        public string Zone { get; set; } = null!;
        public string ParameterName { get; set; } = null!;
        public decimal? TargetValue { get; set; }
        public decimal ActualValue { get; set; }
        public int? UomId { get; set; }
        public DateTime RecordedAt { get; set; }

        [JsonIgnore]
        public virtual BatchStepExecution Execution { get; set; } = null!;
        [JsonIgnore]
        public virtual UnitsOfMeasure? Uom { get; set; }
    }
}
