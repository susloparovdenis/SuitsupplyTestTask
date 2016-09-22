using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using SuitsupplyTestTask.WPFClient.EditProduct;
using SuitsupplyTestTask.WPFClient.Service;

namespace WPFClient.Tests
{
    [TestClass]
    public class EditProductViewModelTests
    {
        private static readonly Mock<DialogService> dialogServiceMock;

        private static readonly Mock<WebApiService> webApiServiceMock;

        static EditProductViewModelTests()
        {
            webApiServiceMock = new Mock<WebApiService>();
            WebApiService.Current = webApiServiceMock.Object;
            dialogServiceMock = new Mock<DialogService>();
            DialogService.Current = dialogServiceMock.Object;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            webApiServiceMock.Reset();
            dialogServiceMock.Reset();
        }

        [TestMethod]
        public void Save_Shows_Error_On_Api_Call_Error()
        {
            webApiServiceMock.Setup(m => m.PostProduct(It.IsAny<ProductDTO>())).Throws<Exception>();
            var editProductViewModel = new EditProductViewModel(new ProductDTO());

            editProductViewModel.Save();

            dialogServiceMock.Verify(m => m.ShowError(It.IsAny<string>()));
        }

        [TestMethod]
        public void Save_Calls_Post_Method_For_Zero_Id()
        {
            webApiServiceMock.Setup(m => m.PostProduct(It.IsAny<ProductDTO>())).Throws<Exception>();
            var editProductViewModel = new EditProductViewModel(new ProductDTO());

            editProductViewModel.Save();

            webApiServiceMock.Verify(m => m.PostProduct(It.IsAny<ProductDTO>()));
        }
    }
}