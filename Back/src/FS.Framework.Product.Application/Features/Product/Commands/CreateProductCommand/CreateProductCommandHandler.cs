using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using FS.Framework.Product.Domain.Entities;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Commands.CreateUserCommand;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _userService;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProductModel>(request);
        entity.Id = Guid.NewGuid();
        entity.Created = DateTime.UtcNow;
        entity.Updated = DateTime.UtcNow;
        var newUser = await _userService.AddAsync(entity);
        var newUserDto = _mapper.Map<ProductDto>(newUser);
        if (newUser != null)
        {
            return new ApiResponse<ProductDto>(newUserDto, "Producto creado exitosamente.");
        }

        return new ApiResponse<ProductDto>(new ProductDto(), "Error al crear el Producto.") { Success = false };
    }
}
