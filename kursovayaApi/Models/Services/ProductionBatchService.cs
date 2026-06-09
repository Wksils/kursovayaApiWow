using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class ProductionBatchService : AbstractionService, ICommonService<ProductionBatch, int>
    {
        private readonly KursovayaContext db;

        public ProductionBatchService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(ProductionBatch model)
        {
            bool result = DoAction(() =>
            {
                db.ProductionBatches.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                ProductionBatch production = db.ProductionBatches.FirstOrDefault(p => p.BatchId == id)!;
                db.ProductionBatches.Remove(production);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<ProductionBatch> Get(int id)
        {
            var res = await db.ProductionBatches.FirstOrDefaultAsync(p => p.BatchId == id);
            return res!;
        }

        public async Task<IEnumerable<ProductionBatch>> GetAll()
        {
            return await db.ProductionBatches.ToListAsync();
        }

        public bool Update(int id, ProductionBatch model)
        {
            bool result = DoAction(() =>
            {
                ProductionBatch production = db.ProductionBatches.FirstOrDefault(p => p.BatchId == id)!;
                production.BatchNumber=model.BatchNumber;
                production.ProductId=model.ProductId;
                production.RecipeId = model.RecipeId;//
                production.CardId=model.CardId;//
                production.PlannedQty=model.PlannedQty;
                production.ActualQty=model.ActualQty;
                production.UomId=model.UomId;
                production.StartedAt=model.StartedAt;
                production.CompletedAt=model.CompletedAt;
                production.StartedBy=model.StartedBy;
                production.CompletedBy=model.CompletedBy;
                production.QaDecision=model.QaDecision;
                production.CreatedAt=model.CreatedAt;
                production.CreatedBy=model.CreatedBy;
                db.ProductionBatches.Update(production);
                db.SaveChanges();
            });
            return result;
        }
    }
}
