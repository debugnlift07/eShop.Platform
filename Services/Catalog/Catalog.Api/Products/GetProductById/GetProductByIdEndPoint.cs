
namespace Catalog.Api.Products.GetProductById
{
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                return Results.Ok(result);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product By Id")
            .WithDescription("Returns a single product by its unique identifier");
        }
    }
}
