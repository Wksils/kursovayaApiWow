using kursovayaApi.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace kursovayaApi.Models.Services
{
    public class ProductService : AbstractionService, ICommonService<Product, int>
    {
        private readonly KursovayaContext db;

        public ProductService(KursovayaContext db)
        {
            this.db = db;
        }

        public bool Create(Product model)
        {
            bool result = DoAction(() =>
            {
                db.Products.Add(model);
                db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(() =>
            {
                Product product = db.Products.FirstOrDefault(p => p.ProductId == id)!;
                db.Products.Remove(product);
                db.SaveChanges();
            });
            return result;
        }

        public async Task<Product> Get(int id)
        {
            var res = await db.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            return res!;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await db.Products.ToListAsync();
        }

        public bool Update(int id, Product model)
        {
            bool result = DoAction(() =>
            {
                Product product = db.Products.FirstOrDefault(p => p.ProductId == id)!;
                product.Code=model.Code;
                product.Name=model.Name;
                product.ProductType=model.ProductType;
                product.ReleaseForm=model.ReleaseForm;
                product.Status=model.Status;
                product.CreatedAt=model.CreatedAt;
                db.Products.Update(product);
                db.SaveChanges();
            });
            return result;
        }
    }
}
