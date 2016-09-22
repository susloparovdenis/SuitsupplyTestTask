using System;

namespace SuitsupplyTestTask.WPFClient.Service
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public decimal Price { get; set; }

        public DateTime LastUpdated { get; set; }

        public ProductDTO Clone()
        {
            return (ProductDTO)MemberwiseClone();
        }
    }
}