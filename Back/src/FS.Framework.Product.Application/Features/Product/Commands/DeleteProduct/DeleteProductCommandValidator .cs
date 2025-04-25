using FluentValidation;

namespace FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del producto es obligatorio.");
    }
}
