using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI.Api.Common;
using SuitsupplyTestTask.WebAPI.Api.Version2.DTO;

namespace SuitsupplyTestTask.WebAPI.Api.Version2
{
    public class ProductsController : ProductsControllerBase<Product>
    {
        public ProductsController(IProductRepository productRepository)
            : base(productRepository)
        {
        }
    }
}