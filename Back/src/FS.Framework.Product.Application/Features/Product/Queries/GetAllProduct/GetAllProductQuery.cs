using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Queries.GetAllProduct;

public record GetAllProductQuery() : IRequest<ApiResponse<IEnumerable<ProductDto>>>;
