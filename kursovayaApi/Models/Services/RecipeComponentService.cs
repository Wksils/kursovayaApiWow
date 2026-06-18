using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class RecipeComponentService : AbstractionService, ICommonService<RecipeComponent, int>
    {
        private readonly KursovayaContext db;

        public RecipeComponentService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(RecipeComponent model)
        {
            bool result = DoAction(() =>
            {
                db.RecipeComponents.Add(model);
                db.SaveChanges();
            });
            return result; 
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                RecipeComponent component = db.RecipeComponents.FirstOrDefault(p => p.ComponentId == id)!;
                db.RecipeComponents.Remove(component);
                db.SaveChanges();
            });
            return result;
        }
        public async Task<IEnumerable<RecipeComponent>> GetByRecipe(int id)
        {
            return await db.RecipeComponents.Where(p => p.RecipeId == id).ToListAsync();
        }
        public async Task<RecipeComponent> Get(int id)
        {
            var res = await db.RecipeComponents.FirstOrDefaultAsync(p => p.ComponentId == id);
            return res!;
        }

        public async Task<IEnumerable<RecipeComponent>> GetAll()
        {
            return await db.RecipeComponents.ToListAsync();
        }

        public bool Update(int id, RecipeComponent model)
        {
            bool result = DoAction(() =>
            {
                RecipeComponent component = db.RecipeComponents.FirstOrDefault(p => p.ComponentId == id)!;
                component.RecipeId = model.RecipeId;
                component.MaterialId = model.MaterialId;
                component.Quantity= model.Quantity;
                component.UomId= model.UomId;
                model.Percentage = model.Percentage;
                model.IsCritical = model.IsCritical;
                db.RecipeComponents.Update(component);
                db.SaveChanges();
            });
            return result;
        }
    }
}
