using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Products;

namespace ShoppingCart.IntegrationTests.Products
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly List<ProductDto> _mockProducts;

        public CustomWebApplicationFactory(List<ProductDto>? mockProducts = null)
        {
            _mockProducts = mockProducts ?? new List<ProductDto>
        {
            new(1, "Laptop", 999.99, "Gaming Laptop", "Electronics", "image_url", new Rating(4.5, 20)),
            new(2, "Smartphone", 699.99, "High-end phone", "Electronics", "image_url", new Rating(3.8, 5))
        };
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IProducts));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var mockProductsClient = new Mock<IProducts>();
                mockProductsClient
                    .Setup(p => p.GetAllProducts())
                    .ReturnsAsync(_mockProducts);

                services.AddSingleton(mockProductsClient.Object);
            });
        }
    }

}
