using ShoppingCart.Domain;

namespace ShoppingCart.Presentation.Services.Cart
{
    public interface ICartService
    {
        Task<List<ProductWithRecommendation>> GetCart();

        Task AddToCart(ProductWithRecommendation product);

        Task RemoveFromCart(int productId);

        Task RemoveSingleItemFromCart(int productId);

        Task ClearCart();
    }
}
