using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(
            Guid Id,
            string Name,
            string Description,
            decimal Price,
            List<string> Category,
            string ImageFile
        ) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Guid Id);

    public class UpdateProductHandler
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IDocumentSession _session;
        private readonly ILogger<UpdateProductHandler> _logger;

        public UpdateProductHandler(
            IDocumentSession session,
            ILogger<UpdateProductHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<UpdateProductResult> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Updating product with Id {ProductId}", request.Id);

            var product = await _session
                .LoadAsync<Product>(request.Id, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(request.Id);

            // ✅ Update fields
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = request.Category;
            product.ImageFile = request.ImageFile;

            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(product.Id);
        }
    }
}
