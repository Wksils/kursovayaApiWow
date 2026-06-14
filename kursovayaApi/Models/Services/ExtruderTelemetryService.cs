using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class ExtruderTelemetryService : AbstractionService, ICommonService<ExtruderTelemetry, int>
    {
        private KursovayaContext db;
        public ExtruderTelemetryService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(ExtruderTelemetry model)
        {
            bool result = DoAction(() =>
            {
                db.ExtruderTelemetries.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                ExtruderTelemetry telemetry = db.ExtruderTelemetries.FirstOrDefault(p => p.TelemetryId == id)!;
                db.ExtruderTelemetries.Remove(telemetry);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<ExtruderTelemetry> Get(int id)
        {
            var res = await db.ExtruderTelemetries.FirstOrDefaultAsync(p => p.TelemetryId == id);
            return res!;
        }

        public async Task<IEnumerable<ExtruderTelemetry>> GetAll()
        {
            return await db.ExtruderTelemetries.ToListAsync();
        }

        public bool Update(int id, ExtruderTelemetry model)
        {
            bool result = DoAction(() =>
            {
                ExtruderTelemetry telemetry = db.ExtruderTelemetries.FirstOrDefault(p => p.TelemetryId == id)!;
                telemetry.ExecutionId = model.ExecutionId;
                telemetry.Zone = model.Zone;
                telemetry.ParameterName = model.ParameterName;
                telemetry.TargetValue = model.TargetValue;
                telemetry.ActualValue = model.ActualValue;
                telemetry.UomId = model.UomId;
                telemetry.RecordedAt = model.RecordedAt;
                db.ExtruderTelemetries.Update(telemetry);
                db.SaveChanges();
            });
            return result;
        }
    }
}
