using Marten;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
       : ICommand<DeleteProductResult>;
    public record DeleteProductResult(Guid Id);
    public class DeleteProductHandler
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _session;
        private readonly ILogger<DeleteProductHandler> _logger;

        public DeleteProductHandler(
            IDocumentSession session,
            ILogger<DeleteProductHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<DeleteProductResult> Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Deleting product with Id {ProductId}", request.Id);

            var product = await _session
                .LoadAsync<Product>(request.Id, cancellationToken);

            if (product is null)
                throw new KeyNotFoundException(
                    $"Product with Id {request.Id} not found");

            _session.Delete<Product>(request.Id);
            await _session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(request.Id);
        }
    }
}
