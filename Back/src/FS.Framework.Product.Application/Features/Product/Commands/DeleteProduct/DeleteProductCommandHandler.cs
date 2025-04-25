using MediatR;

namespace FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly IProductService _productService;
        public DeleteProductCommandHandler(IProductService productService) => _productService = productService;

        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(request.Id);
            return result > 0
                ? new ApiResponse<string>("Producto eliminado exitosamente.")
                : new ApiResponse<string>("No se pudo eliminar el Producto.") { Success = false };
        }
    }
}
