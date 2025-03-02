using ShoppingCart.Application.Dto;

namespace ShoppingCart.Application.Recommendation
{
    public interface IRecommendationEngine
    {
        bool IsProductRecommended(ProductDto product);
    }
}
