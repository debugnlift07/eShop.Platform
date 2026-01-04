using Catalog.Api.Products.CreateProduct;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

//for CQRS- saggregrate command and query
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<CreateProductCommand>();
});

//fuluent validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//marten for db
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
var app = builder.Build();

//carter for routing 
app.MapCarter();

app.Run();
