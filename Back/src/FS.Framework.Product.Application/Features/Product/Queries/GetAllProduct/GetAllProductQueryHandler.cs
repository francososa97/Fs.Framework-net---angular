using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ApiResponse<IEnumerable<ProductDto>>>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync();
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return new ApiResponse<IEnumerable<ProductDto>>(productsDto);
    }
}
