using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class ExtruderEventService : AbstractionService, ICommonService<ExtruderEvent, int>
    {
        private readonly KursovayaContext db;

        public ExtruderEventService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(ExtruderEvent model)
        {
            bool result = DoAction(() =>
            {
                db.ExtruderEvents.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                ExtruderEvent extruder = db.ExtruderEvents.FirstOrDefault(p => p.EventId == id)!;
                db.ExtruderEvents.Remove(extruder);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<ExtruderEvent> Get(int id)
        {
            var res = await db.ExtruderEvents.FirstOrDefaultAsync(p => p.EventId == id);
            return res!;
        }

        public async Task<IEnumerable<ExtruderEvent>> GetAll()
        {
            return await db.ExtruderEvents.ToListAsync();
        }

        public bool Update(int id, ExtruderEvent model)
        {
            bool result = DoAction(() =>
            {
                ExtruderEvent extruder = db.ExtruderEvents.FirstOrDefault(p => p.EventId == id)!;
                extruder.ExecutionId = model.ExecutionId;
                extruder.Zone = model.Zone;
                extruder.EventType = model.EventType;
                extruder.ParameterName = model.ParameterName;
                extruder.ActualValue = model.ActualValue;
                extruder.TargetValue = model.TargetValue;
                extruder.Description = model.Description;
                extruder.RecordedBy = model.RecordedBy;
                extruder.RecordedAt = model.RecordedAt;
                db.ExtruderEvents.Update(extruder);
                db.SaveChanges();
            });
            return result;
        }
    }
}
