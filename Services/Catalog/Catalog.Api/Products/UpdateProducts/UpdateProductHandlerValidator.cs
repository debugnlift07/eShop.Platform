using Catalog.Api.Products.UpdateProduct;

namespace Catalog.Api.Products.UpdateProducts
{
    public class UpdateProductHandlerValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductHandlerValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Product name is required")
                    .MaximumLength(100);

            RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Product description is required")
                    .MaximumLength(500);

            RuleFor(x => x.Category)
                    .NotEmpty().WithMessage("Product category is required")
                    .Must(c => c.Count > 0).WithMessage("Atleast one category is required");

            RuleFor(x => x.ImageFile)
                    .NotEmpty()
                    .Must(BeAValidImage)
                    .WithMessage("Image file must be a valid image");

            RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than zero");
        }
        private bool BeAValidImage(string imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            return allowedExtensions.Any(ext => imageFile.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
   
}
