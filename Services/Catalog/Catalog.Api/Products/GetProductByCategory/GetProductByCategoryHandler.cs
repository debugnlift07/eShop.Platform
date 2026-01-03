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
        private readonly ILogger<GetProductByCategoryHandler> _logger;

        public GetProductByCategoryHandler(
            IDocumentSession session,
            ILogger<GetProductByCategoryHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<GetProductByCategoryResult> Handle(
            GetProductByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Handling GetProductByCategoryQuery for Category {Category}",
                request.Category);

            var products = await _session
                .Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
