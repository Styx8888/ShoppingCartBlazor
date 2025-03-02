namespace ShoppingCart.Application.Dto
{
    public record Rating(double Rate, int Count);

    public record ProductDto(
        int Id,
        string Title,
        double Price,
        string Description,
        string Category,
        string Image,
        Rating Rating);

}
