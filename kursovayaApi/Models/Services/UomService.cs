using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class UomService : AbstractionService, ICommonService<UnitsOfMeasure, int>
    {
        private readonly KursovayaContext db;

        public UomService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(UnitsOfMeasure model)
        {
            bool result = DoAction(() =>
            {
                db.UnitsOfMeasures.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                UnitsOfMeasure uom = db.UnitsOfMeasures.FirstOrDefault(p => p.UomId == id)!;
                db.UnitsOfMeasures.Remove(uom);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<UnitsOfMeasure> Get(int id)
        {
            var res = await db.UnitsOfMeasures.FirstOrDefaultAsync(p => p.UomId == id)!;
            return res!;
        }

        public async Task<IEnumerable<UnitsOfMeasure>> GetAll()
        {
            return await db.UnitsOfMeasures.ToListAsync();
        }

        public bool Update(int id, UnitsOfMeasure model)
        {
            bool result = DoAction(() =>
            {
                UnitsOfMeasure uom = db.UnitsOfMeasures.FirstOrDefault(p => p.UomId == id)!;
                uom.Symbol = model.Symbol;
                uom.Name = model.Name;
                db.UnitsOfMeasures.Update(uom);
                db.SaveChanges();
            });
            return result;
        }
    }
}
