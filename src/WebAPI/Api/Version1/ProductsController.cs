using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using NLog;

using Omu.ValueInjecter;

using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI.Api.Common;
using SuitsupplyTestTask.WebAPI.Api.Version1.DTO;

namespace SuitsupplyTestTask.WebAPI.Api.Version1
{
    public class ProductsController : ProductsControllerBase<Product>
    {
        public ProductsController(IProductRepository productRepository)
            : base(productRepository)
        {
        }
    }
}