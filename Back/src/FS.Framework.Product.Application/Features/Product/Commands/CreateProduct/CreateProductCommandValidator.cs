using FluentValidation;

namespace FS.Framework.Product.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
    }
}
