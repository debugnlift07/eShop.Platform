namespace Catalog.Api.Products.DeleteProduct
{
    public class DeleteProductHandlerValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductHandlerValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Product Id is required");
        }
    }
}
