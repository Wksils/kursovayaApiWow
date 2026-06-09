using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class MaterialBatchService : AbstractionService, ICommonService<MaterialBatch, int>
    {
        private readonly KursovayaContext db;

        public MaterialBatchService(KursovayaContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool Create(MaterialBatch model)
        {
            bool result = DoAction(() =>
            {
                db.MaterialBatches.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                MaterialBatch material = db.MaterialBatches.FirstOrDefault(p => p.BatchId == id)!;
                db.MaterialBatches.Remove(material);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<MaterialBatch> Get(int id)
        {
            var res = await db.MaterialBatches.FirstOrDefaultAsync(p => p.BatchId == id);
            return res!;
        }

        public async Task<IEnumerable<MaterialBatch>> GetAll()
        {
            return await db.MaterialBatches.ToListAsync();
        }

        public bool Update(int id, MaterialBatch model)
        {
            bool result = DoAction(() =>
            {
                MaterialBatch material = db.MaterialBatches.FirstOrDefault(p => p.BatchId == id)!;
                material.MaterialId = model.MaterialId;
                material.BatchNumber=model.BatchNumber;
                material.Supplier=model.Supplier;
                material.Quantity=model.Quantity;
                material.UomId=model.UomId;
                material.ManufactureDate=model.ManufactureDate;
                material.ExpiryDate=model.ExpiryDate;
                material.ReceivedAt=model.ReceivedAt;
                material.Status=model.Status;
                db.MaterialBatches.Update(material);
                db.SaveChanges();
            });
            return result;
        }
    }
}
