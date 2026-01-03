
namespace Catalog.Api.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> products);
    public class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Handle  called with {@Query}");

            var product= await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductResult(product);
        }
    }
}
