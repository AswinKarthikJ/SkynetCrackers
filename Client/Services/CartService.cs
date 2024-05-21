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
        Task<List<CartItem>> GetCartItems();
        Task DeleteItem(CartItem cartItem);
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

            //var isProductExistsInCart = cart.FirstOrDefault
            //    (c => c.Id == product.Id);

            //if (isProductExistsInCart is null)
               cart.Add(product);
            //else
            //    isProductExistsInCart.Quantity += 1;

            await _localStorage.SetItemAsync("cart", cart);

            _toastService.ShowSuccess($"Added to cart : {product.Title}");

            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return new List<CartItem>();
            }

            var items = new List<CartItem>();

            foreach(var item in cart)
            {
                var itemExistsInCart = items.FirstOrDefault(c => c.Id == item.Id);
                if (itemExistsInCart is null)
                {
                    items.Add(new CartItem
                    {
                        Id = item.Id,
                        Image = item.Image,
                        Price = item.Price,
                        Title = item.Title,
                        Quantity = 1,
                    });
                }
                else
                {
                    itemExistsInCart.Quantity += 1;
                }
            }

            return items;
        }

        public async Task DeleteItem(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.Id == item.Id);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }
    }
}
