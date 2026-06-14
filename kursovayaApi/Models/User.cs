using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace kursovayaApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Department { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<BatchStepExecution>? BatchStepExecutionCompletedByNavigations { get; set; } = new List<BatchStepExecution>();
    [JsonIgnore]
    public virtual ICollection<BatchStepExecution>? BatchStepExecutionStartedByNavigations { get; set; } = new List<BatchStepExecution>();
    [JsonIgnore]
    public virtual ICollection<LabTest>? LabTestAssignedToNavigations { get; set; } = new List<LabTest>();
    [JsonIgnore]
    public virtual ICollection<LabTest>? LabTestDecisionByNavigations { get; set; } = new List<LabTest>();
    [JsonIgnore]
    public virtual ICollection<ProductionBatch>? ProductionBatchCompletedByNavigations { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<ProductionBatch>? ProductionBatchCreatedByNavigations { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<ProductionBatch>? ProductionBatchStartedByNavigations { get; set; } = new List<ProductionBatch>();
    [JsonIgnore]
    public virtual ICollection<Recipe>? RecipeApprovedByNavigations { get; set; } = new List<Recipe>();
    [JsonIgnore]
    public virtual ICollection<Recipe>? RecipeCreatedByNavigations { get; set; } = new List<Recipe>();
    [JsonIgnore]
    public virtual ICollection<TechCard>? TechCardApprovedByNavigations { get; set; } = new List<TechCard>();
    [JsonIgnore]
    public virtual ICollection<TechCard>? TechCardCreatedByNavigations { get; set; } = new List<TechCard>();
    [JsonIgnore]
    public virtual ICollection<ExtruderEvent> ExtruderEvents { get; set; } = new List<ExtruderEvent>();
}
