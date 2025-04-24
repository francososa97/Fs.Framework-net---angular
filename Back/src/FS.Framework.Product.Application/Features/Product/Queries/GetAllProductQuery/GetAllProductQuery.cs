using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Queries.GetAllUsersQuery;

public record GetAllProductQuery() : IRequest<ApiResponse<IEnumerable<ProductDto>>>;
