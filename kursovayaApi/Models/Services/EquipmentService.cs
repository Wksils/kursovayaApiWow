using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class EquipmentService : AbstractionService, ICommonService<Equipment, int>
    {
        private readonly KursovayaContext db;

        public EquipmentService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(Equipment model)
        {
            bool result = DoAction(delegate ()
            {
                db.Equipment.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Equipment equipment = db.Equipment.FirstOrDefault(p => p.EquipmentId == id)!;
                db.Equipment.Remove(equipment);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<Equipment> Get(int id)
        {
            var res = await db.Equipment.FirstOrDefaultAsync(p => p.EquipmentId == id);
            return res!;
        }

        public async Task<IEnumerable<Equipment>> GetAll()
        {
            return await db.Equipment.ToListAsync();
        }

        public bool Update(int id, Equipment model)
        {
            bool result = DoAction(delegate ()
            {
                Equipment equipment = db.Equipment.FirstOrDefault(p => p.EquipmentId == id)!;
                equipment.Code = model.Code;
                equipment.Name = model.Name;
                equipment.Department =model.Department;
                equipment.Status =model.Status;
                db.Equipment.Update(equipment);
                db.SaveChanges();
            });
            return result;
        }
    }
}
