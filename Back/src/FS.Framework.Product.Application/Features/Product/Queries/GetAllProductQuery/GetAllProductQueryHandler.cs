using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Queries.GetAllUsersQuery;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ApiResponse<IEnumerable<ProductDto>>>
{
    private readonly IProductService _userService;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IProductService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _userService.GetAllAsync();
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return new ApiResponse<IEnumerable<ProductDto>>(productsDto);
    }
}
