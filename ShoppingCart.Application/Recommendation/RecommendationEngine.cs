using ShoppingCart.Application.Dto;

namespace ShoppingCart.Application.Recommendation
{
    public class RecommendationEngine : IRecommendationEngine
    {
        public bool IsProductRecommended(ProductDto product)
        {
            if (product.Price >= 20 && product.Rating.Rate >= 4.0)
            {
                return true;
            }

            return false;
        }
    }
}
