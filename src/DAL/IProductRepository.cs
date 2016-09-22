using System.Linq;
using System.Threading.Tasks;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();

        Task<Product> FindAsync(int id);

        /// <summary>
        ///     Updates exisitng product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true if found</returns>
        Task Update(Product product);

        Task Insert(Product product);

        Task Delete(int id);
    }
}