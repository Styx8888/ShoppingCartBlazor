using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Pagination;
using ShoppingCart.Application.Products;
using ShoppingCart.Application.Products.Mappers;
using ShoppingCart.Application.Recommendation;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Services
{
    public class ProductWithRecommendationService : IProductWithRecommendationService
    {
        private readonly IProducts _productsApiClient;
        private readonly IMemoryCache _cache;
        private readonly IPaginationService _paginationService;
        private readonly IRecommendationEngine _recommendationEngine;
        private readonly string productCacheKey = "Products";

        public ProductWithRecommendationService(IProducts productsApiClient, IMemoryCache cache, IPaginationService paginationService, IRecommendationEngine recommendationEngine)
        {
            _productsApiClient = productsApiClient;
            _cache = cache;
            _paginationService = paginationService;
            _recommendationEngine = recommendationEngine;
        }

        public async Task<PaginationResult<ProductWithRecommendation>> GetProductsWithRecommendation(int page, int pageSize)
        {
            var allProducts = await GetAllProductsAsync();

            var productsWithRecommendation = allProducts.ToProductsWithRecommendation(_recommendationEngine);

            return _paginationService.Paginate(productsWithRecommendation, page, pageSize);
        }

        private async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            if (_cache.TryGetValue(productCacheKey, out List<ProductDto> cachedProducts))
            {
                return cachedProducts;
            }
            
            var products = await _productsApiClient.GetAllProducts();

            _cache.Set(productCacheKey, products);

            return products;
        }
    }
}
