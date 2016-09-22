using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Caliburn.Micro;

using SuitsupplyTestTask.WPFClient.EditProduct;
using SuitsupplyTestTask.WPFClient.Service;

namespace SuitsupplyTestTask.WPFClient.MainWindow
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            Task.Factory.StartNew(LoadProducts);
        }

        public BindableCollection<ProductViewModel> ProductViewModels { get; } = new BindableCollection<ProductViewModel>();

        private async void LoadProducts()
        {
            var products = await WebApiService.Current.GetProducts();
            foreach (var productDto in products)
                ProductViewModels.Add(new ProductViewModel(productDto));
            //            ProductViewModels.AddRange(products.Select(ProductViewModel.Create));
        }

        public void Add()
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