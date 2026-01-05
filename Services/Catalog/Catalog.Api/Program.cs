

using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);


//for CQRS- saggregrate command and query
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));

});

//fuluent validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

//carter for routing 
builder.Services.AddCarter();

//marten for db
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
