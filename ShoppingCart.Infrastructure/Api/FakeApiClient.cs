using Flurl.Http;
using Flurl.Http.Configuration;
using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Products;

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
            try
            {
                var products = await _fakeApiClient.Request("products").GetJsonAsync<IEnumerable<ProductDto>>();
                return products;
            }
            catch (Exception)
            {
                //// TODO: Logger should log the error here, or implement some strategy for retrying
                return Enumerable.Empty<ProductDto>();
            }
           
        }
    }
}
