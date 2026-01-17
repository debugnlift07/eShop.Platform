using Carter;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//carter for routing 
builder.Services.AddCarter();
app.Run();
