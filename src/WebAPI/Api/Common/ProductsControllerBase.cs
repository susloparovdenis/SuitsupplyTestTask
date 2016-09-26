using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

using NLog;

using Omu.ValueInjecter;

using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI.Api.Version1.DTO;

namespace SuitsupplyTestTask.WebAPI.Api.Common
{
    public abstract class ProductsControllerBase<TDto> : ApiController where TDto:IHaveId
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IProductRepository productRepository;

        protected ProductsControllerBase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public static TDto Map(DAL.Model.Product product) => Mapper.Map<TDto>(product);

        public static DAL.Model.Product Map(TDto product) => Mapper.Map<DAL.Model.Product>(product);

        /// <summary>
        /// Returns a list of products.
        /// </summary>
        public IQueryable<TDto> GetProducts()
        {
            logger.Info("Get products called");
            return productRepository.GetAll().Select(Map).AsQueryable(); //TODO IQUERABLE
        }

        /// <summary>
        /// Finds a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await productRepository.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(Map(product));
        }

        // PUT: api/Products/5
        /// <summary>
        /// Modifies an existing product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>some</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, TDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                var entity = Map(product);
                await productRepository.Update(entity);
                return Ok(Map(entity));

            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

        }

        // POST: api/Products
        /// <summary>
        /// Creates a new product.
        /// </summary>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(TDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await productRepository.Insert(Map(product));

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Deletes a product.
        /// </summary>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            try
            {
                await productRepository.Delete(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
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