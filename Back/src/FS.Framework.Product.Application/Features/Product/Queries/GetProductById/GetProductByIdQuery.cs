using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ApiResponse<ProductDto>>;
