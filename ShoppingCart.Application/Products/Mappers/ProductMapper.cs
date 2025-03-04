using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Recommendation;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Products.Mappers
{
    public static class ProductMapper
    {
        public static IEnumerable<ProductWithRecommendation> ToProductsWithRecommendation(
            this IEnumerable<ProductDto> productDtos,
            IRecommendationEngine recommendationEngine)
        {
            return productDtos.Select(dto => new ProductWithRecommendation(
                dto.Id,
                dto.Title,
                dto.Price,
                dto.Description,
                dto.Category,
                dto.Image,
                (int)Math.Round(dto.Rating.Rate),
                recommendationEngine.IsProductRecommended(dto)
            )).ToList();
        }
    }

}
