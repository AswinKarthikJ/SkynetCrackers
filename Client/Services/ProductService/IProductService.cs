using SkynetCrackers.Shared;

namespace SkynetCrackers.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action OnChange;
        List<Product> products { get; set; }
        Task LoadProducts(string categoryUrl = null);
        Task<Product> GetProduct(int id);
    }
}
