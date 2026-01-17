
using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
//routing 
builder.Services.AddCarter();

//handle CQRS 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapCarter();
app.Run();
