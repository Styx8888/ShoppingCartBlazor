using Flurl.Http;
using Flurl.Http.Configuration;
using ShoppingCart.Domain;

namespace ShoppingCart.Presentation.Services.Products
{
    public class ProductService : IProductService
    {
        private IFlurlClient _productsApiClient;

        public ProductService(IFlurlClientCache clients)
        {
            _productsApiClient = clients.Get("ProductsApiClient");
        }

        public async Task<PaginationResult<ProductWithRecommendation>> GetProductsAsync(int page, int pageSize)
        {
            return await _productsApiClient.Request("products").AppendQueryParam("Page", page).AppendQueryParam("PageSize", pageSize).GetJsonAsync<PaginationResult<ProductWithRecommendation>>();
            // return await _productsApiClient.Request("products").GetJsonAsync<PaginationResult<ProductWithRecommendation>>();
        }
    }
}
