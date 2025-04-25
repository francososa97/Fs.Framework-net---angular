using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var existing = await _productService.GetByIdAsync(request.Id);
        if (existing == null)
            throw new KeyNotFoundException("Producto no encontrado");

        _mapper.Map(request, existing);
        existing.Updated = DateTime.UtcNow;

        var response = await _productService.UpdateAsync(existing);
        var newproductDto = _mapper.Map<ProductDto>(response);

        return new ApiResponse<ProductDto>(newproductDto, "Producto actualizado correctamente.");

    }
}
