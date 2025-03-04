using System.Net.Http.Json;
using System.Net;
using ShoppingCart.Domain;
using Shouldly;
using ShoppingCart.Application.Dto;

namespace ShoppingCart.IntegrationTests.Products
{
    public class ProductApiTests
    {
        [Fact]
        public async Task GetProducts_ShouldReturnPaginatedProducts()
        {
            
            using var factory = new CustomWebApplicationFactory(new List<ProductDto>
            {
            new(1, "Laptop", 999.99, "Gaming Laptop", "Electronics", "image_url", new Rating(4.5, 20)),
            new(2, "Smartphone", 699.99, "High-end phone", "Electronics", "image_url", new Rating(3.8, 5))
            });

            var client = factory.CreateClient();

            var response = await client.GetAsync("/products?page=1&pageSize=1");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<PaginationResult<ProductWithRecommendation>>();
            result.ShouldNotBeNull();
            result.Data.ShouldHaveSingleItem();
            result.TotalItems.ShouldBe(2);
        }

        [Fact]
        public async Task GetProducts_ShouldReturn404_WhenNoProductsAvailable()
        {
            var factory = new CustomWebApplicationFactory(new List<ProductDto>());
            var client = factory.CreateClient();

            var response = await client.GetAsync("/products?page=1&pageSize=1");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
