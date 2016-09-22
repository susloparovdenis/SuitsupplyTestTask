using System.Collections.Generic;
using System.Data.Entity;
using System.Net;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public class ProductContextInitializer : DropCreateDatabaseIfModelChanges<ProductsContext>
    {
        public static readonly List<Product> entities = new List<Product>
                       {
                           new Product { Id = 1, Name = "havana grey check", Price = 299},
                           new Product { Id = 2, Name = "lazio blue herringbonert", Price = 379},
                           new Product { Id = 3, Name = "lazio blue stripe" , Price = 429}
                       };

        private byte[] GetImgage(string uri)
        {
            using (WebClient client = new WebClient())
            {
                return  client.DownloadData(uri); //TODO: async
            }
        }

        protected override void Seed(ProductsContext productsContext)
        {
            entities[0].Photo = GetImgage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Grey_Check_Havana_P4959_Suitsupply_Online_Store_1.jpg");
            entities[1].Photo = GetImgage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Blue_Herringbone_Lazio_P4928_Suitsupply_Online_Store_1.jpg");
            entities[2].Photo = GetImgage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Blue_Stripe_Lazio_P4933_Suitsupply_Online_Store_1.jpg");

           
            productsContext.Products.AddRange(entities);
        }
    }
}