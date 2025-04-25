using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Commands.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, double Price, int Stock) : IRequest<ApiResponse<ProductDto>>;


