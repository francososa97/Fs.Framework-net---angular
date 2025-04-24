using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Queries.GetUserByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<ProductDto>>
{
    private readonly IProductService _userService;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IMapper mapper, IProductService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ApiResponse<ProductDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _userService.GetByIdAsync(request.Id);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto is null
            ? new ApiResponse<ProductDto>(false, "Usuario no encontrado.")
            : new ApiResponse<ProductDto>(productDto);
    }
}
