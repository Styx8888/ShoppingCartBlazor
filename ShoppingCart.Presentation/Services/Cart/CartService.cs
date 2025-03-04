namespace ShoppingCart.Presentation.Services.Cart
{
    using Blazored.LocalStorage;
    using ShoppingCart.Domain;

    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private const string CartKey = "cart";

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<ProductWithRecommendation>> GetCart()
        {
            return await _localStorage.GetItemAsync<List<ProductWithRecommendation>>(CartKey) ?? new();
        }

        public async Task AddToCart(ProductWithRecommendation product)
        {
            var cart = await GetCart();
            cart.Add(product);
            await _localStorage.SetItemAsync(CartKey, cart);
        }

        public async Task RemoveFromCart(int productId)
        {
            var cart = await GetCart();
            cart.RemoveAll(p => p.Id == productId);
            await _localStorage.SetItemAsync(CartKey, cart);
        }

        public async Task ClearCart()
        {
            await _localStorage.RemoveItemAsync(CartKey);
        }
    }

}
