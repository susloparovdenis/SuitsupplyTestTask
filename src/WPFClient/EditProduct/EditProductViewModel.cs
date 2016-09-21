using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using Caliburn.Micro;

using SuitsupplyTestTask.WPFClient.Service;

namespace SuitsupplyTestTask.WPFClient.EditProduct
{
    public class EditProductViewModel: Screen
    {
        public override string DisplayName => "Edit";

        public bool CanSave
        {
            get
            {
                if (string.IsNullOrEmpty(ProductName))
                    return false;
                decimal _;
                if (!decimal.TryParse(price, NumberStyles.Currency,CultureInfo.InvariantCulture, out _))
                    return false;
                if (!string.IsNullOrEmpty(imagePath) && !File.Exists(imagePath))
                    return false;
                return true;
            }
        }

        private readonly ProductDTO productDto;

        private string imagePath;

        private string price;

        public EditProductViewModel()
        {
        }

        public EditProductViewModel(ProductDTO productDto)
        {
            this.productDto = productDto;
            Price = productDto.Price.ToString(CultureInfo.InvariantCulture);
        }

        public string ProductName { get { return productDto.Name; }
            set
            {
                productDto.Name = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanSave));
            }
        }

        public string Price { get { return price; }
            set
            {
                price= value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanSave));
            } }

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

        public ProductDTO ProductDto => productDto;

        public async void Save()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                productDto.Price = decimal.Parse(price, NumberStyles.Currency, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    //TODO: check not image
                    productDto.Photo = File.ReadAllBytes(ImagePath);
                }
                if(productDto.Id == 0)
                    await WebApiService.Current.PostProduct(productDto);
                else
                    await WebApiService.Current.PutProduct(productDto.Id, productDto);

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

        public bool CanCancel { get; set; } = true;
        public void Cancel()
        {
            TryClose(false);
        }
    }
}
