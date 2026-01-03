using Carter;
using MediatR;

namespace Catalog.Api.Products.DeleteProduct
{
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (
                Guid id,
                ISender sender) =>
            {
                var result = await sender.Send(
                    new DeleteProductCommand(id));

                return Results.Ok(result);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Deletes a product by its ID");
        }
    }
}
