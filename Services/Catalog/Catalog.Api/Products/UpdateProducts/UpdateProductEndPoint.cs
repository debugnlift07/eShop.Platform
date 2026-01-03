namespace Catalog.Api.Products.UpdateProduct
{
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id:guid}", async (
                Guid id,
                UpdateProductCommand command,
                ISender sender) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Product ID mismatch");

                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Updates an existing product by its ID");
        }
    }
}
