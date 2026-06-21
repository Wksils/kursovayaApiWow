using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class LabTestService : AbstractionService, ICommonService<LabTest, int>
    {
        private readonly KursovayaContext db;

        public LabTestService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(LabTest model)
        {
            bool result = DoAction(delegate()
            {
                db.LabTests.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                LabTest lab = db.LabTests.FirstOrDefault(p => p.TestId == id)!;
                db.LabTests.Remove(lab);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<LabTest> Get(int id)
        {
            var res = await db.LabTests.FirstOrDefaultAsync(p => p.TestId == id);
            return res!;
        }

        public async Task<IEnumerable<LabTest>> GetAll()
        {
            return await db.LabTests.ToListAsync();
        }

        public bool Update(int id, LabTest model)
        {
            bool result = DoAction(() =>
            {
                LabTest lab = db.LabTests.FirstOrDefault(p => p.TestId == id)!;
                lab.BatchId = model.BatchId;
                lab.MatBatchId = model.MatBatchId;
                lab.TestType=model.TestType;
                lab.Status=model.Status;
                lab.AssignedTo=model.AssignedTo;
                lab.StartedAt=model.StartedAt;
                lab.CompletedAt=model.CompletedAt;
                lab.ResultsText=model.ResultsText;
                lab.OverallResult=model.OverallResult;
                lab.DecisionBy=model.DecisionBy;
                lab.DecisionAt=model.DecisionAt;
                lab.CreatedAt=model.CreatedAt;
                lab.Priority = model.Priority;
                lab.Comment = model.Comment;
                lab.ControlledParameters = model.ControlledParameters;
                db.LabTests.Update(lab);
                db.SaveChanges();
            });
            return result;
        }
    }
}
