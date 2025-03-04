using ShoppingCart.Domain;

namespace ShoppingCart.Application.Services
{
    public interface IProductWithRecommendationService
    {
        Task<PaginationResult<ProductWithRecommendation>> GetProductsWithRecommendation(int page, int pageSize);
    }
}
