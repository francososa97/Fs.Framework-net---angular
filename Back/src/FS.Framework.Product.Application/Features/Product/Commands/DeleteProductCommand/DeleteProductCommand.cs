using MediatR;
namespace FS.Framework.Product.Application.Features.Users.Commands.DeleteUserCommand;

public record DeleteProductCommand(Guid Id) : IRequest<ApiResponse>;
