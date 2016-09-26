using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using NLog;

namespace SuitsupplyTestTask.WPFClient.Service
{
    public class WebApiService
    {
        private const string baseURL = "http://suitsupplytest.azurewebsites.net/api/v1/";

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static WebApiService current;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        protected WebApiService()
        {
            jsonSerializerSettings = new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture };
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());
        }

        public static WebApiService Current
        {
            get { return current ?? (current = new WebApiService()); }
            set
            {
                if (current != null)
                    throw new InvalidOperationException();
                current = value;
            }
        }

        private async Task<T> RestCall<T>(string method, MethodType methodType, object body = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                var serializeObject = JsonConvert.SerializeObject(body, Formatting.None, jsonSerializerSettings);
                var stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                switch (methodType)
                {
                    case MethodType.GET:
                        response = await client.GetAsync(method);
                        break;
                    case MethodType.POST:
                        response = await client.PostAsync(method, stringContent);
                        break;
                    case MethodType.PUT:
                        response = await client.PutAsync(method, stringContent);
                        break;
                    case MethodType.DELETE:
                        response = await client.DeleteAsync(method);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(methodType), methodType, null);
                }
                if (!response.IsSuccessStatusCode)
                {
                    logger.Error($"Failed request, reason phrase:\n{response.ReasonPhrase}\n\ncontent:\n{response.Content}");
                    throw new Exception(response.ReasonPhrase);
                }
                var text = await response.Content.ReadAsStringAsync();
                logger.Trace($"Got json:\n{text}");
                return JsonConvert.DeserializeObject<T>(text);
            }
        }

        public virtual async Task<ProductDTO[]> GetProducts() => await RestCall<ProductDTO[]>("products", MethodType.GET);

        public virtual async Task<ProductDTO> GetProduct(int id) => await RestCall<ProductDTO>($"products/{id}", MethodType.GET);

        public virtual async Task<ProductDTO> DeleteProduct(int id) => await RestCall<ProductDTO>($"products/{id}", MethodType.DELETE);

        public virtual async Task<ProductDTO> PostProduct(ProductDTO product) => await RestCall<ProductDTO>("products/", MethodType.POST, product);

        public virtual async Task<ProductDTO> PutProduct(int id, ProductDTO product) => await RestCall<ProductDTO>($"products/{id}", MethodType.PUT, product);

        private enum MethodType
        {
            GET,

            POST,

            PUT,

            DELETE
        }
    }
}