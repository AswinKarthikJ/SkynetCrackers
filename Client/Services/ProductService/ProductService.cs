using SkynetCrackers.Shared;
using System.Net.Http.Json;

namespace SkynetCrackers.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<Product> products { get; set; } = new List<Product>();

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task LoadProducts(string categoryUrl = null)
        {
            if (categoryUrl == null)
            {
                products = await _http.GetFromJsonAsync<List<Product>>("api/Product");
            }
            else
            {
                products = await _http.GetFromJsonAsync<List<Product>>($"api/Product/Category/{categoryUrl}");
            }
            OnChange.Invoke();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _http.GetFromJsonAsync<Product>($"api/Product/{id}");
        }
    }
}
