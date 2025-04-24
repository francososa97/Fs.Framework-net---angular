using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Commands.CreateUserCommand;

public record CreateProductCommand(string Name, double Price, int Stock) : IRequest<ApiResponse<ProductDto>>;
