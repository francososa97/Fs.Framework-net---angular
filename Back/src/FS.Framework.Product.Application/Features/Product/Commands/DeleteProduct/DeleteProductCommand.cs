using MediatR;
namespace FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<ApiResponse>;
