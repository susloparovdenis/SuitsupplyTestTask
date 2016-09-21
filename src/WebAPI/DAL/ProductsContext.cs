using System;
using System.Data.Entity;
using System.Linq;

using SuitsupplyTestTask.WebAPI.DAL.Model;

namespace SuitsupplyTestTask.WebAPI.DAL
{
    public class ProductsContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            // Update LastUpdated field on changed or created Products
            var updatedOrCreated = ChangeTracker.Entries<Product>()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            foreach (var entry in updatedOrCreated)
            {
                entry.Entity.LastUpdated = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
}