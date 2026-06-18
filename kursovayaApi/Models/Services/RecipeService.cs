using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class RecipeService : AbstractionService, ICommonService<Recipe, int>
    {
        private readonly KursovayaContext db;

        public RecipeService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(Recipe model)
        {
            bool result = DoAction(() =>
            {
                db.Recipes.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                Recipe recipe = db.Recipes.FirstOrDefault(p => p.RecipeId == id)!;
                db.Recipes.Remove(recipe);
                db.SaveChanges();
            });
            return result;
        }
        public bool Archive(int id)
        {
            bool res = DoAction(() =>
            {
                Recipe recipe = db.Recipes.FirstOrDefault(p => p.RecipeId == id)!;
                recipe.IsActive = false;
                recipe.Status = "archived";
                db.Recipes.Update(recipe);
                db.SaveChanges();
            });
            return res;
        }

        public async Task<Recipe> Get(int id)
        {
            var res = await db.Recipes.FirstOrDefaultAsync(p => p.RecipeId == id);
            return res!;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            return await db.Recipes.ToListAsync();
        }

        public bool Update(int id, Recipe model)
        {
            bool result = DoAction(() =>
            {
                Recipe recipe = db.Recipes.FirstOrDefault(p => p.RecipeId == id)!;
                recipe.ProductId = model.ProductId;
                recipe.Version=model.Version;
                recipe.Status=model.Status;
                recipe.IsActive=model.IsActive;
                recipe.ApprovedBy=model.ApprovedBy;
                recipe.ApprovedAt=model.ApprovedAt;
                recipe.CreatedAt=model.CreatedAt;
                recipe.CreatedBy=model.CreatedBy;
                recipe.Notes=model.Notes;
                db.Recipes.Update(recipe);
                db.SaveChanges();
            });
            return result;
        }
    }
}
