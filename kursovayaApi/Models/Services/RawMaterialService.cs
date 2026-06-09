using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class RawMaterialService : AbstractionService, ICommonService<RawMaterial, int>
    {
        private readonly KursovayaContext db;

        public RawMaterialService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(RawMaterial model)
        {
            bool result = DoAction(() =>
            {
                db.RawMaterials.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                RawMaterial material = db.RawMaterials.FirstOrDefault(p=>p.MaterialId==id)!;
                db.RawMaterials.Remove(material);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<RawMaterial> Get(int id)
        {
            var res = await db.RawMaterials.FirstOrDefaultAsync(p => p.MaterialId == id);
            return res!;
        }

        public async Task<IEnumerable<RawMaterial>> GetAll()
        {
            return await db.RawMaterials.ToListAsync();
        }

        public bool Update(int id, RawMaterial model)
        {
            bool result = DoAction(() =>
            {
                RawMaterial material = db.RawMaterials.FirstOrDefault(p => p.MaterialId == id)!;
                material.Code=model.Code;
                material.Name=model.Name;
                material.Category=model.Category;
                material.UomId=model.UomId;
                material.ShelfLifeDays=model.ShelfLifeDays;
                material.Status=model.Status;
                db.RawMaterials.Update(material);
                db.SaveChanges();
            });
            return result;
        }
    }
}
