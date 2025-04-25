using FluentValidation;

namespace FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct;

public class DeleteUserCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del producto es obligatorio.");
    }
}
