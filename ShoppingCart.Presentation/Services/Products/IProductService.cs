using ShoppingCart.Domain;

namespace ShoppingCart.Presentation.Services.Products
{
    public interface IProductService
    {
        Task<PaginationResult<ProductWithRecommendation>> GetProductsAsync(int page, int pageSize);
    }
}
