using MediatR;

namespace FS.Framework.Product.Application.Features.Users.Commands.DeleteUserCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly IProductService _userService;
        public DeleteProductCommandHandler(IProductService userService) => _userService = userService;

        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteAsync(request.Id);
            return result > 0
                ? new ApiResponse<string>("Producto eliminado exitosamente.")
                : new ApiResponse<string>("No se pudo eliminar el Producto.") { Success = false };
        }
    }
}
