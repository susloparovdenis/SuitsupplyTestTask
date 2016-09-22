using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using NLog;

using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI.Controllers.DTO;

namespace SuitsupplyTestTask.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        ///     Returns a list of products.
        /// </summary>
        public IQueryable<Product> GetProducts()
        {
            logger.Info("Get products called");
            return productRepository.GetAll().Select(Product.Map).AsQueryable(); //TODO IQUERABLE
        }

        /// <summary>
        ///     Finds a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await productRepository.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(Product.Map(product));
        }

        // PUT: api/Products/5
        /// <summary>
        ///     Modifies an existing product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>some</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.Id)
                return BadRequest();

            var isFound = await productRepository.Update(Product.Map(product));
            if (!isFound)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        /// <summary>
        ///     Creates a new product.
        /// </summary>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await productRepository.Insert(Product.Map(product));

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        ///     Deletes a product.
        /// </summary>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.FindAsync(id);
            if (product == null)
                return NotFound();

            await productRepository.Delete(product.Id);

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var disposable = productRepository as IDisposable;
                disposable?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}