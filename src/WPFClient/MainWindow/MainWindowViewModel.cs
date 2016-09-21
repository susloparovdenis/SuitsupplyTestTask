using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Caliburn.Micro;

using Newtonsoft.Json.Linq;

using SuitsupplyTestTask.WPFClient.EditProduct;
using SuitsupplyTestTask.WPFClient.Service;

namespace SuitsupplyTestTask.WPFClient.MainWindow
{
    public class MainWindowViewModel : PropertyChangedBase
    {

        public BindableCollection<ProductViewModel> ProductViewModels { get; } = new BindableCollection<ProductViewModel>();

        public MainWindowViewModel()
        {
            Task.Factory.StartNew(LoadProducts);

        }

        private async void LoadProducts()
        {
            var products = await WebApiService.Current.GetProducts();
            foreach (var productDto in products)
            {
                ProductViewModels.Add(new ProductViewModel(productDto));
                //                await Task.Delay(1000);
            }
            //            ProductViewModels.AddRange(products.Select(ProductViewModel.Create));
        }

        public async void Add()
        {
            var editProductViewModel = new EditProductViewModel(new ProductDTO());
            var result = DialogService.Current.ShowDialog(editProductViewModel);
            if (result != true)
                return;
            ProductViewModels.Add(new ProductViewModel(editProductViewModel.ProductDto));
        }
        public async void Delete(ProductViewModel productViewModel)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                await WebApiService.Current.DeleteProduct(productViewModel.Id);
                ProductViewModels.Remove(productViewModel);

            }
            catch (Exception e)
            {
                DialogService.Current.ShowError(e.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
