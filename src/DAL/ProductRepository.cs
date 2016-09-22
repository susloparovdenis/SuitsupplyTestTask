using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly ProductsContext context = new ProductsContext();

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<Product> GetAll() => context.Products;

        public Task<Product> FindAsync(int id) => context.Products.FindAsync(id);

        /// <summary>
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true if found</returns>
        public async Task<bool> Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                    return false;
                throw;
            }
            return true;
        }

        public async Task Insert(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id) => await context.Products.FindAsync(id);

        private bool ProductExists(int id)
        {
            return context.Products.Count(e => e.Id == id) > 0;
        }
    }
}