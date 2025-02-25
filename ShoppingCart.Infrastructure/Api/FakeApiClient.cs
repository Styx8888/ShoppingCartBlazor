using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Interfaces;

namespace ShoppingCart.Infrastructure.Api
{
    public class FakeApiClient : IProducts
    {
        public Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
