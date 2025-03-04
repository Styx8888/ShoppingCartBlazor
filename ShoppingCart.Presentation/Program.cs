using Flurl.Http.Configuration;
using ShoppingCart.Presentation.Components;
using ShoppingCart.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var productsApiUrl = builder.Configuration.GetValue<string>("ApiSettings:ProductsApiUrl");

if (string.IsNullOrWhiteSpace(productsApiUrl))
{
    throw new InvalidOperationException("Configuration error: ApiSettings:ProductsApiUrl is missing or empty in appsettings.json");
}

builder.Services.AddSingleton(sp => new FlurlClientCache()
    .Add("ProductsApiClient", productsApiUrl));
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
