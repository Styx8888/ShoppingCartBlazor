using ShoppingCart.Application.Dto;

namespace ShoppingCart.Application.Interfaces
{
    public interface IProducts
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
    }
}
