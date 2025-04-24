using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Queries.GetUserByIdQuery;

public record GetUserByIdQuery(Guid Id) : IRequest<ApiResponse<ProductDto>>;
