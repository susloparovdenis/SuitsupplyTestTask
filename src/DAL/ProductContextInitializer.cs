using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;

using SuitsupplyTestTask.DAL.Model;

namespace SuitsupplyTestTask.DAL
{
    public class ProductContextInitializer : DropCreateDatabaseIfModelChanges<ProductsContext>
    {
        public static readonly List<Product> entities = new List<Product>
                                                        {
                                                            new Product { Id = 1, Name1 = "havana grey check", Price = 299 },
                                                            new Product { Id = 2, Name1 = "lazio blue herringbonert", Price = 379 },
                                                            new Product { Id = 3, Name1 = "lazio blue stripe", Price = 429 }
                                                        };

        private async Task<byte[]> GetImage(string uri)
        {
            using (var client = new WebClient())
            {
                var downloadDataTaskAsync = await client.DownloadDataTaskAsync(uri);
                return downloadDataTaskAsync; 
            }
        }

        protected override void Seed(ProductsContext productsContext)
        {
            var t1 = GetImage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Grey_Check_Havana_P4959_Suitsupply_Online_Store_1.jpg");
            var t2 = GetImage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Blue_Herringbone_Lazio_P4928_Suitsupply_Online_Store_1.jpg");
            var t3 = GetImage("http://statics.suitsupply.com/images/products/Suits/large/Suits_Blue_Stripe_Lazio_P4933_Suitsupply_Online_Store_1.jpg");
            entities[0].Photo = t1.Result;
            entities[1].Photo = t2.Result;
            entities[2].Photo = t3.Result;
            productsContext.Products.AddRange(entities);
        }
    }
}