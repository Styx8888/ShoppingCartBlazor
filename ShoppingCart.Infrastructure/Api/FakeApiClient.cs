using Flurl.Http;
using Flurl.Http.Configuration;
using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Interfaces;

namespace ShoppingCart.Infrastructure.Api
{
    public class FakeApiClient : IProducts
    {
        private readonly IFlurlClient _fakeApiClient;

        public FakeApiClient(IFlurlClientCache clients)
        {
            _fakeApiClient = clients.Get("FakeApiClient");
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _fakeApiClient.Request("products").GetJsonAsync<IEnumerable<ProductDto>>();
            return products;
        }
    }
}
