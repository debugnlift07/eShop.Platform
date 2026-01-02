using Catalog.Api.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<CreateProductCommand>();
});

var app = builder.Build();

app.MapCarter();

app.Run();
