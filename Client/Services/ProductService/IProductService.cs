using SkynetCrackers.Shared;

namespace SkynetCrackers.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action OnChange;
        List<Product> Products { get; set; }
        void LoadProducts();
    }
}
