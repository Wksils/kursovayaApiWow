using System.Text.Json.Serialization;

namespace kursovayaApi.Models
{
    public partial class ExtruderEvent
    {
        public int EventId { get; set; }
        public int ExecutionId { get; set; }
        public string Zone { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string? ParameterName { get; set; }
        public decimal? ActualValue { get; set; }
        public decimal? TargetValue { get; set; }
        public string? Description { get; set; }
        public int? RecordedBy { get; set; }
        public DateTime RecordedAt { get; set; }

        [JsonIgnore]
        public virtual BatchStepExecution Execution { get; set; } = null!;
        [JsonIgnore]
        public virtual User? RecordedByUser { get; set; }
    }
}
