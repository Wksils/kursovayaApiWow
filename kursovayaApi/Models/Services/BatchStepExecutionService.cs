using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class BatchStepExecutionService : AbstractionService, ICommonService<BatchStepExecution, int>
    {
        private readonly KursovayaContext db;
        public BatchStepExecutionService(KursovayaContext db)
        {
            this.db = db;
        }
        public bool Create(BatchStepExecution model)
        {
            bool result = DoAction(delegate ()
            {
                db.BatchStepExecutions.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                BatchStepExecution batch = db.BatchStepExecutions.FirstOrDefault(p => p.ExecutionId == id)!;
                db.BatchStepExecutions.Remove(batch);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<BatchStepExecution> Get(int id)
        {
            var res = await db.BatchStepExecutions.FirstOrDefaultAsync(p => p.ExecutionId == id);
            return res!;
        }

        public async Task<IEnumerable<BatchStepExecution>> GetAll()
        {
            return await db.BatchStepExecutions.ToListAsync();
        }

        public bool Update(int id, BatchStepExecution model)
        {
            bool result = DoAction(delegate (){
                BatchStepExecution batch = db.BatchStepExecutions.FirstOrDefault(p => p.ExecutionId == id)!;
                batch.BatchId = model.BatchId;
                batch.StepId = model.StepId;
                batch.Status = model.Status;
                batch.StartedAt = model.StartedAt;
                batch.CompletedAt = model.CompletedAt;
                batch.StartedBy = model.StartedBy;
                batch.CompletedBy = model.CompletedBy; 
                batch.ActualParams = model.ActualParams;
                batch.Notes = model.Notes;
                db.BatchStepExecutions.Update(batch);
                db.SaveChanges();
            });
            return result;
        }
    }
}
