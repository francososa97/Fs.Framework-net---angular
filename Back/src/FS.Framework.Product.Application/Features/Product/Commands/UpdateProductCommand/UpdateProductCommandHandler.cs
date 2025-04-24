using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Commands.UpdateUserCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductDto>>
{
    private readonly IProductService _userService;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var existing = await _userService.GetByIdAsync(request.Id);
        if (existing == null)
            throw new KeyNotFoundException("Producto no encontrado");

        _mapper.Map(request, existing);
        existing.Updated = DateTime.UtcNow;

        var response = await _userService.UpdateAsync(existing);
        var newUserDto = _mapper.Map<ProductDto>(response);

        return new ApiResponse<ProductDto>(newUserDto, "Producto actualizado correctamente.");

    }
}
