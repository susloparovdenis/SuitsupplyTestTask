using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using SuitsupplyTestTask.DAL;
using SuitsupplyTestTask.WebAPI.Api.Version2.DTO;

using ProductsController = SuitsupplyTestTask.WebAPI.Api.Version2.ProductsController;

namespace SuitsupplyTestTask.WebAPI.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> productContextMock = new Mock<IProductRepository>();

        [TestInitialize]
        public void TestInitialize()
        {
            var entities = ProductContextInitializer.entities;
            productContextMock.Setup(m => m.GetAll()).Returns(entities.AsQueryable());
            productContextMock.Setup(m => m.FindAsync(It.IsAny<int>())).Returns<int>(i => Task.FromResult(entities.SingleOrDefault(e => e.Id == i)));
        }

        [TestMethod]
        public async Task GetProduct_Returns_Not_found()
        {
            var productsController = new ProductsController(productContextMock.Object);
            var result = await productsController.GetProduct(4);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProduct_Returns_OK()
        {
            var productsController = new ProductsController(productContextMock.Object);
            var result = await productsController.GetProduct(1);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Product>));
        }
    }
}