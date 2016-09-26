using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public class ProductsContext : DbContext
    {
        static ProductsContext()
        {
            Database.SetInitializer(new ProductContextInitializer());
        }

        public DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            // Update LastUpdated field on changed or created Products
            SetLastUpdated();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            SetLastUpdated();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SetLastUpdated();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetLastUpdated()
        {
            var updatedOrCreated = ChangeTracker.Entries<Product>()
                .Where(x => (x.State == EntityState.Modified) || (x.State == EntityState.Added));
            foreach (var entry in updatedOrCreated)
                entry.Entity.LastUpdated = DateTime.UtcNow;
            
        }
    }
}