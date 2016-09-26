using System;
using System.Globalization;
using System.IO;
using System.Windows.Input;

using Caliburn.Micro;

using SuitsupplyTestTask.WPFClient.Service;

namespace SuitsupplyTestTask.WPFClient.EditProduct
{
    public class EditProductViewModel : Screen
    {
        private string imagePath;

        private string price;

        public EditProductViewModel()
        {
        }

        public EditProductViewModel(ProductDTO productDto)
        {
            ProductDto = productDto;
            Price = productDto.Price.ToString(CultureInfo.InvariantCulture);
            DisplayName = "Edit";
        }

        public bool CanSave
        {
            get
            {
                if (string.IsNullOrEmpty(ProductName))
                    return false;
                decimal _;
                if (!decimal.TryParse(price, NumberStyles.Currency, CultureInfo.InvariantCulture, out _))
                    return false;
                if (!string.IsNullOrEmpty(imagePath) && !File.Exists(imagePath))
                    return false;
                return true;
            }
        }

        public string ProductName
        {
            get { return ProductDto.Name; }
            set
            {
                ProductDto.Name = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value == imagePath) return;
                imagePath = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        public ProductDTO ProductDto { get; set; }

        public bool CanCancel { get; set; } = true;

        public async void Save()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                ProductDto.Price = decimal.Parse(price, NumberStyles.Currency, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(imagePath))
                    ProductDto.Photo = File.ReadAllBytes(ImagePath);
                if (ProductDto.Id == 0)
                    ProductDto = await WebApiService.Current.PostProduct(ProductDto);
                else
                    ProductDto = await WebApiService.Current.PutProduct(ProductDto.Id, ProductDto);
            }
            catch (Exception e)
            {
                DialogService.Current.ShowError(e.Message);
                return;
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}