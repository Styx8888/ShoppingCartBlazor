using AutoFixture;
using ShoppingCart.Application.Dto;
using ShoppingCart.Application.Pagination;
using Shouldly;

namespace ShoppingCart.UnitTests.Pagination
{
    public class PaginationTests
    {
        private readonly Fixture _fixture;

        public PaginationTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Paginate_ExampleDto_ReturnsCorrectLimitedData()
        {
            var sampleData = _fixture.CreateMany<ProductDto>(20);
            var paginationService = new PaginationService();
            var paginationResult = paginationService.Paginate(sampleData, 1, 5);
            paginationResult.Data.Count.ShouldBe(5);
            paginationResult.Page.ShouldBe(1);
            paginationResult.PageSize.ShouldBe(5);
            paginationResult.TotalPages.ShouldBe(4);
        }
    }
}