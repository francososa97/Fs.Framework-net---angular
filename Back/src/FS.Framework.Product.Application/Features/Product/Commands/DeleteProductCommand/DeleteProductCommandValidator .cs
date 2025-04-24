using FluentValidation;

namespace FS.Framework.Product.Application.Features.Users.Commands.DeleteUserCommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del usuario es obligatorio.");
    }
}
