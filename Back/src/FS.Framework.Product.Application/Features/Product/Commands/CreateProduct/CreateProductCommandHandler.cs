using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using FS.Framework.Product.Domain.Entities;
using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Commands.CreateProduct;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductModel>(request);
        entity.Id = Guid.NewGuid();
        entity.Created = DateTime.UtcNow;
        entity.Updated = DateTime.UtcNow;
        var newProduct = await _productService.AddAsync(entity);
        var newProductDto = _mapper.Map<ProductDto>(newProduct);
        if (newProduct != null)
        {
            return new ApiResponse<ProductDto>(newProductDto, "Producto creado exitosamente.");
        }

        return new ApiResponse<ProductDto>(new ProductDto(), "Error al crear el Producto.") { Success = false };
    }
}
