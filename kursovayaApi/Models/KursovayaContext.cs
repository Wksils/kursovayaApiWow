using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models;

public partial class KursovayaContext : DbContext
{
    public KursovayaContext()
    {
        Database.EnsureCreated();
    }

    public KursovayaContext(DbContextOptions<KursovayaContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<BatchStepExecution> BatchStepExecutions { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<MaterialBatch> MaterialBatches { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductionBatch> ProductionBatches { get; set; }

    public virtual DbSet<RawMaterial> RawMaterials { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeComponent> RecipeComponents { get; set; }

    public virtual DbSet<TechCard> TechCards { get; set; }

    public virtual DbSet<TechStep> TechSteps { get; set; }

    public virtual DbSet<UnitsOfMeasure> UnitsOfMeasures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        // Оставляем пустым, строку будем брать из appsettings.json
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BatchStepExecution>(entity =>
        {
            entity.HasKey(e => e.ExecutionId).HasName("PK__batch_st__360496AA959153C4");

            entity.ToTable("batch_step_executions");

            entity.HasIndex(e => new { e.BatchId, e.StepId }, "uq_batch_step").IsUnique();

            entity.Property(e => e.ExecutionId).HasColumnName("execution_id");
            entity.Property(e => e.ActualParams)
                .HasMaxLength(500)
                .HasColumnName("actual_params");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CompletedBy).HasColumnName("completed_by");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.StartedBy).HasColumnName("started_by");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.StepId).HasColumnName("step_id");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchStepExecutions)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__batch_ste__batch__245D67DE");

            entity.HasOne(d => d.CompletedByNavigation).WithMany(p => p.BatchStepExecutionCompletedByNavigations)
                .HasForeignKey(d => d.CompletedBy)
                .HasConstraintName("FK__batch_ste__compl__29221CFB");

            entity.HasOne(d => d.StartedByNavigation).WithMany(p => p.BatchStepExecutionStartedByNavigations)
                .HasForeignKey(d => d.StartedBy)
                .HasConstraintName("FK__batch_ste__start__282DF8C2");

            entity.HasOne(d => d.Step).WithMany(p => p.BatchStepExecutions)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__batch_ste__step___25518C17");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__equipmen__197068AFD4726E2E");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.Code, "UQ__equipmen__357D4CF9D9954591").IsUnique();

            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("operational")
                .HasColumnName("status");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__lab_test__F3FF1C02086BB8E4");

            entity.ToTable("lab_tests");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.DecisionAt).HasColumnName("decision_at");
            entity.Property(e => e.DecisionBy).HasColumnName("decision_by");
            entity.Property(e => e.OverallResult)
                .HasMaxLength(10)
                .HasColumnName("overall_result");
            entity.Property(e => e.ResultsText)
                .HasMaxLength(2000)
                .HasColumnName("results_text");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TestType)
                .HasMaxLength(20)
                .HasColumnName("test_type");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.LabTestAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__lab_tests__assig__2FCF1A8A");

            entity.HasOne(d => d.Batch).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__lab_tests__batch__2BFE89A6");

            entity.HasOne(d => d.DecisionByNavigation).WithMany(p => p.LabTestDecisionByNavigations)
                .HasForeignKey(d => d.DecisionBy)
                .HasConstraintName("FK__lab_tests__decis__31B762FC");
        });

        modelBuilder.Entity<MaterialBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__material__DBFC04315406DDA3");

            entity.ToTable("material_batches");

            entity.HasIndex(e => e.BatchNumber, "UQ__material__56E378378850C18A").IsUnique();

            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(100)
                .HasColumnName("batch_number");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.ManufactureDate).HasColumnName("manufacture_date");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("quantity");
            entity.Property(e => e.ReceivedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("received_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("quarantine")
                .HasColumnName("status");
            entity.Property(e => e.Supplier)
                .HasMaxLength(200)
                .HasColumnName("supplier");
            entity.Property(e => e.UomId).HasColumnName("uom_id");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__material___mater__6B24EA82");

            entity.HasOne(d => d.Uom).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.UomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__material___uom_i__6D0D32F4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF52A60ED2C");

            entity.ToTable("products");

            entity.HasIndex(e => e.Code, "UQ__products__357D4CF9422BF4C0").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ProductType)
                .HasMaxLength(30)
                .HasColumnName("product_type");
            entity.Property(e => e.ReleaseForm)
                .HasMaxLength(30)
                .HasColumnName("release_form");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .HasColumnName("status");
        });

        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__producti__DBFC0431608849CE");

            entity.ToTable("production_batches", tb => tb.HasTrigger("trg_batch_validate"));

            entity.HasIndex(e => e.BatchNumber, "UQ__producti__56E3783742E1E93C").IsUnique();

            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.ActualQty)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("actual_qty");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(100)
                .HasColumnName("batch_number");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CompletedBy).HasColumnName("completed_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.PlannedQty)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("planned_qty");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.QaDecision)
                .HasMaxLength(20)
                .HasColumnName("qa_decision");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.StartedBy).HasColumnName("started_by");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("planned")
                .HasColumnName("status");
            entity.Property(e => e.UomId).HasColumnName("uom_id");

            entity.HasOne(d => d.Card).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productio__card___17F790F9");

            entity.HasOne(d => d.CompletedByNavigation).WithMany(p => p.ProductionBatchCompletedByNavigations)
                .HasForeignKey(d => d.CompletedBy)
                .HasConstraintName("FK__productio__compl__1DB06A4F");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionBatchCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productio__creat__1F98B2C1");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productio__produ__160F4887");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productio__recip__17036CC0");

            entity.HasOne(d => d.StartedByNavigation).WithMany(p => p.ProductionBatchStartedByNavigations)
                .HasForeignKey(d => d.StartedBy)
                .HasConstraintName("FK__productio__start__1CBC4616");

            entity.HasOne(d => d.Uom).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.UomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__productio__uom_i__19DFD96B");
        });

        modelBuilder.Entity<RawMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__raw_mate__6BFE1D283F8C386A");

            entity.ToTable("raw_materials");

            entity.HasIndex(e => e.Code, "UQ__raw_mate__357D4CF93051D127").IsUnique();

            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.Category)
                .HasMaxLength(30)
                .HasColumnName("category");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ShelfLifeDays).HasColumnName("shelf_life_days");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.UomId).HasColumnName("uom_id");

            entity.HasOne(d => d.Uom).WithMany(p => p.RawMaterials)
                .HasForeignKey(d => d.UomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__raw_mater__uom_i__5EBF139D");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__recipes__3571ED9BDBF3FC65");

            entity.ToTable("recipes", tb =>
                {
                    tb.HasTrigger("trg_recipe_pct_check");
                    tb.HasTrigger("trg_recipe_single_active");
                });

            entity.HasIndex(e => new { e.ProductId, e.Version }, "uq_recipe_version").IsUnique();

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("draft")
                .HasColumnName("status");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .HasColumnName("version");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.RecipeApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__recipes__approve__778AC167");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RecipeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipes__created__787EE5A0");

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipes__product__73BA3083");
        });

        modelBuilder.Entity<RecipeComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__recipe_c__AEB1DA5959658312");

            entity.ToTable("recipe_components");

            entity.HasIndex(e => new { e.RecipeId, e.MaterialId }, "uq_recipe_material").IsUnique();

            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.IsCritical).HasColumnName("is_critical");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(7, 4)")
                .HasColumnName("percentage");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("quantity");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.UomId).HasColumnName("uom_id");

            entity.HasOne(d => d.Material).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_co__mater__7E37BEF6");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__recipe_co__recip__7D439ABD");

            entity.HasOne(d => d.Uom).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.UomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipe_co__uom_i__00200768");
        });

        modelBuilder.Entity<TechCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__tech_car__BDF201DD4CE8352F");

            entity.ToTable("tech_cards", tb => tb.HasTrigger("trg_card_single_active"));

            entity.HasIndex(e => new { e.ProductId, e.Version }, "uq_card_version").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("draft")
                .HasColumnName("status");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .HasColumnName("version");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.TechCardApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__tech_card__appro__09A971A2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TechCardCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tech_card__creat__0A9D95DB");

            entity.HasOne(d => d.Product).WithMany(p => p.TechCards)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tech_card__produ__05D8E0BE");
        });

        modelBuilder.Entity<TechStep>(entity =>
        {
            entity.HasKey(e => e.StepId).HasName("PK__tech_ste__B2E1DE81B9BB93AA");

            entity.ToTable("tech_steps");

            entity.HasIndex(e => new { e.CardId, e.StepNumber }, "uq_step_order").IsUnique();

            entity.Property(e => e.StepId).HasColumnName("step_id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.DurationMin).HasColumnName("duration_min");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.IsCritical).HasColumnName("is_critical");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ParamsNote)
                .HasMaxLength(500)
                .HasColumnName("params_note");
            entity.Property(e => e.StepNumber).HasColumnName("step_number");

            entity.HasOne(d => d.Card).WithMany(p => p.TechSteps)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__tech_step__card___0F624AF8");

            entity.HasOne(d => d.Equipment).WithMany(p => p.TechSteps)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK__tech_step__equip__10566F31");
        });

        modelBuilder.Entity<UnitsOfMeasure>(entity =>
        {
            entity.HasKey(e => e.UomId).HasName("PK__units_of__48EF04DF2964A8D3");

            entity.ToTable("units_of_measure");

            entity.HasIndex(e => e.Symbol, "UQ__units_of__DF7EEB81750F86A9").IsUnique();

            entity.Property(e => e.UomId).HasColumnName("uom_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F5A024CC3");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "UQ__users__7838F27298B596E7").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .HasDefaultValue("operator")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
