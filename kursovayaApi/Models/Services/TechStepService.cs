using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class TechStepService : AbstractionService, ICommonService<TechStep, int>
    {
        private readonly KursovayaContext db;

        public TechStepService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(TechStep model)
        {
            bool result = DoAction(() =>
            {
                db.TechSteps.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                TechStep step = db.TechSteps.FirstOrDefault(p=>p.StepId==id)!;
                db.TechSteps.Remove(step);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<TechStep> Get(int id)
        {
            var res = await db.TechSteps.FirstOrDefaultAsync(p => p.StepId == id);
            return res!;
        }

        public async Task<IEnumerable<TechStep>> GetAll()
        {
            return await db.TechSteps.ToListAsync();
        }

        public bool Update(int id, TechStep model)
        {
            bool result = DoAction(() =>
            {
                TechStep step = db.TechSteps.FirstOrDefault(p => p.StepId == id)!;
                step.CardId = model.CardId;
                step.StepNumber= model.StepNumber;
                step.Name= model.Name;
                step.Description= model.Description;
                step.Equipment= model.Equipment;
                step.DurationMin= model.DurationMin;
                step.IsCritical= model.IsCritical;
                step.ParamsNote= model.ParamsNote;
                db.TechSteps.Update(step);
                db.SaveChanges();
            });
            return result;
        }
    }
}
