using System;
using System.IO;
using System.Windows.Media.Imaging;

using Caliburn.Micro;

using SuitsupplyTestTask.WPFClient.EditProduct;
using SuitsupplyTestTask.WPFClient.Service;

namespace SuitsupplyTestTask.WPFClient
{
    public class ProductViewModel : PropertyChangedBase
    {
        private ProductDTO productDto;

        public ProductViewModel(ProductDTO productDto)
        {
            SetProduct(productDto);
        }

        public string ProductName => productDto.Name;

        public int Id => productDto.Id;

        public decimal Price => productDto.Price;

        public DateTime LastUpdated => productDto.LastUpdated;

        public BitmapImage BitmapImage
        {
            get
            {
                if (productDto.Photo == null)
                    return null;
                var byteStream = new MemoryStream(productDto.Photo);
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = byteStream;
                bi.EndInit();
                return bi;
            }
        }

        private void SetProduct(ProductDTO productDto)
        {
            this.productDto = productDto;
            Refresh();
        }

        public void Edit()
        {
            var editProductViewModel = new EditProductViewModel(productDto.Clone());
            var result = DialogService.Current.ShowDialog(editProductViewModel);
            if (result != true)
                return;
            SetProduct(editProductViewModel.ProductDto);
        }

        public static ProductViewModel Create(ProductDTO productDto)
        {
            return new ProductViewModel(productDto);
        }
    }
}