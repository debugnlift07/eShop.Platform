namespace Catalog.Api.Products.GetProductByCategory
{
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (
                string category,
                ISender sender) =>
            {
                var result = await sender.Send(
                    new GetProductByCategoryQuery(category));

                return Results.Ok(result);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Returns all products that belong to a given category");
        }
    }
}
