namespace ShoppingCart.Domain
{
    public record ProductWithRecommendation(
        int Id,
        string Title,
        double Price,
        string Description,
        string Category,
        string Image,
        int Stars,
        bool Recommendation);
}
