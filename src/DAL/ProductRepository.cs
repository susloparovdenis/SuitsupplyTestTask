using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public class EntityNotFoundException : Exception
    {
    }

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
        public async Task Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                    throw new EntityNotFoundException();
                throw;
            }
        }

        public async Task Insert(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await FindAsync(id);
            if (product == null)
                throw new EntityNotFoundException();

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        private bool ProductExists(int id)
        {
            return context.Products.Count(e => e.Id == id) > 0;
        }
    }
}