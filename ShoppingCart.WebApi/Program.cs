using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Pagination;
using ShoppingCart.Application.Products;
using ShoppingCart.Application.Recommendation;
using ShoppingCart.Application.Services;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure.Api;
using ShoppingCart.WebApi.Request;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache(options =>
{
    options.CompactionPercentage = 0.2;
    options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
});

var fakeApiUrl = builder.Configuration.GetValue<string>("ApiSettings:FakeApiUrl");

if (string.IsNullOrWhiteSpace(fakeApiUrl))
{
    throw new InvalidOperationException("Configuration error: ApiSettings:FakeApiUrl is missing or empty in appsettings.json");
}

builder.Services.AddSingleton(sp => new FlurlClientCache()
    .Add("FakeApiClient", fakeApiUrl));

builder.Services.AddSingleton<IPaginationService, PaginationService>();
builder.Services.AddSingleton<IRecommendationEngine, RecommendationEngine>();
builder.Services.AddScoped<IProducts, FakeApiClient>();
builder.Services.AddScoped<IProductWithRecommendationService, ProductWithRecommendationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (
    [FromServices] IProductWithRecommendationService productService,
    [AsParameters] PaginationParams pagination) =>
{
    var result = await productService.GetProductsWithRecommendation(pagination.Page, pagination.PageSize);

    if (result.Data.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
})
.WithName("GetProductsWithRecommendations")
.Produces<PaginationResult<ProductWithRecommendation>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError)
.Produces(StatusCodes.Status404NotFound);

app.Run();

public partial class Program { }

