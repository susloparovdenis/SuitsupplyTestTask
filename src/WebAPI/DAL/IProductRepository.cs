using System.Linq;
using System.Threading.Tasks;

using SuitsupplyTestTask.WebAPI.DAL.Model;

namespace SuitsupplyTestTask.WebAPI.DAL
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();

        Task<Product> FindAsync(int id);

        /// <summary>
        /// Updates exisitng product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>true if found</returns>
        Task<bool> Update(Product product);

        Task Insert(Product product);

        Task Delete(int id);
    }
}