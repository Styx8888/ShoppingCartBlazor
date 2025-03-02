using Flurl.Http;
using Flurl.Http.Configuration;
using Moq;
using ShoppingCart.Infrastructure.Api;
using Shouldly;

namespace ShoppingCartBlazor.IntegrationTests.FakeApi
{
    public class ClientTests
    {
        [Fact]
        public async Task GetAllProducts_ExampleClient_ReturnsAllProductsAsDtos()
        {
            var clientStub = new FlurlClient("https://fakestoreapi.com");
            var clientFactoryMock = new Mock<IFlurlClientCache>();
            clientFactoryMock.Setup(x => x.Get(It.IsAny<string>())).Returns(clientStub);

            var fakeApiService = new FakeApiClient(clientFactoryMock.Object);

            var products = await fakeApiService.GetAllProducts();

            products.ShouldNotBeNull();
        }
    }
}