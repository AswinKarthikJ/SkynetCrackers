using Blazored.LocalStorage;
using Blazored.Toast.Services;
using SkynetCrackers.Client.Services.ProductService;
using SkynetCrackers.Shared;

namespace SkynetCrackers.Client.Services
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Product product);
    }

    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorage,
            IToastService toastService,
            IProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
        }


        public async Task AddToCart(Product product)
        {
            var cart = await _localStorage.GetItemAsync<List<Product>>("cart");

            if (cart == null)
            {
                cart = new List<Product>();
            }

            cart.Add(product);
            await _localStorage.SetItemAsync("cart", cart);

            _toastService.ShowSuccess($"Added to cart : {product.Title}");

            OnChange.Invoke();
        }
    }
}
