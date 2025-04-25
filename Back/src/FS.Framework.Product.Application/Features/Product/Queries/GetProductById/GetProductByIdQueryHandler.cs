using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using FS.Framework.Product.Application.Features.Product.Queries.GetProductById;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Queries.GetproductByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<ApiResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(request.Id);
        var productDto = _mapper.Map<ProductDto>(product);
        return productDto is null
            ? new ApiResponse<ProductDto>(false, "producto no encontrado.")
            : new ApiResponse<ProductDto>(productDto);
    }
}
