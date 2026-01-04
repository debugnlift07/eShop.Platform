
namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<String> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //validation
            var result = await validator.ValidateAsync(command, cancellationToken);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            //create product entity using cammand
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            //save to database
            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);
            //retrun create product result
            return new CreateProductResult(product.Id);

        }
    }
}
