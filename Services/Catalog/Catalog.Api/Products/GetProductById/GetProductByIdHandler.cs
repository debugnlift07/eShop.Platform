namespace Catalog.Api.Products.GetProductById
{
    // ✅ Query (Request)
    public record GetProductByIdQuery(Guid Id)
        : IQuery<GetProductByIdResult>;

    // ✅ Result (Response)
    public record GetProductByIdResult(Product Product);

    // ✅ Handler
    public class GetProductByIdHandler
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IDocumentSession _session;
        private readonly ILogger<GetProductByIdHandler> _logger;

        public GetProductByIdHandler(
            IDocumentSession session,
            ILogger<GetProductByIdHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<GetProductByIdResult> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Handling GetProductByIdQuery for Id {ProductId}",
                request.Id);

            var product = await _session
                .Query<Product>()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null)
                throw new InvalidOperationException(
                    $"Product with Id {request.Id} not found");

            return new GetProductByIdResult(product);
        }
    }
}
