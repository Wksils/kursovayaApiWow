using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class TechCardService : AbstractionService, ICommonService<TechCard, int>
    {
        private readonly KursovayaContext db;

        public TechCardService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(TechCard model)
        {
            bool result = DoAction(() =>
            {
                db.TechCards.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                TechCard card = db.TechCards.FirstOrDefault(p => p.CardId == id)!;
                db.TechCards.Remove(card);
                db.SaveChanges();
            });
            return result;
        }
        public bool Archive(int id)
        {
            bool res = DoAction(() =>
            {
                TechCard card = db.TechCards.FirstOrDefault(p => p.CardId == id)!;
                card.IsActive = false;
                db.TechCards.Update(card);
                db.SaveChanges();
            });
            return res;
        }


        public async Task<TechCard> Get(int id)
        {
            var res = await db.TechCards.FirstOrDefaultAsync(p=>p.CardId == id);
            return res!;
        }

        public async Task<IEnumerable<TechCard>> GetAll()
        {
            return await db.TechCards.ToListAsync();
        }

        public bool Update(int id, TechCard model)
        {
            bool result = DoAction(() =>
            {
                TechCard card = db.TechCards.FirstOrDefault(p => p.CardId == id)!;
                card.ProductId = model.ProductId;
                card.Version=model.Version;
                card.Status=model.Status;
                card.IsActive=model.IsActive;
                card.ApprovedAt=model.ApprovedAt;
                card.ApprovedBy=model.ApprovedBy;
                card.CreatedAt=model.CreatedAt;
                card.CreatedBy=model.CreatedBy;
                card.Notes=model.Notes;
                db.TechCards.Update(card);
                db.SaveChanges();
            });
            return result;
        }
    }
}
