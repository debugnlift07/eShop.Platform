namespace Catalog.Api.Products.GetProductByCategory
{
    // ✅ Query (Request)
    public record GetProductByCategoryQuery(string Category)
        : IQuery<GetProductByCategoryResult>;

    // ✅ Result (Response)
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    // ✅ Handler
    public class GetProductByCategoryHandler
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IDocumentSession _session;

        public GetProductByCategoryHandler(
            IDocumentSession session,
            ILogger<GetProductByCategoryHandler> logger)
        {
            _session = session;
        }

        public async Task<GetProductByCategoryResult> Handle(
            GetProductByCategoryQuery request,
            CancellationToken cancellationToken)
        {
          

            var products = await _session
                .Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
