using FS.Framework.Product.Domain.Entities;

/// <summary>
/// Servicio de aplicación para la gestión de Producto.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Obtiene todos los Producto activos.
    /// </summary>
    Task<IEnumerable<ProductModel>> GetAllAsync();

    /// <summary>
    /// Obtiene un producto por ID.
    /// </summary>
    Task<ProductModel?> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo producto.
    /// </summary>
    Task<ProductModel> AddAsync(ProductModel product);

    /// <summary>
    /// Actualiza un producto existente.
    /// </summary>
    Task<ProductModel> UpdateAsync(ProductModel product);

    /// <summary>
    /// Marca un producto como eliminado.
    /// </summary>
    Task<int> DeleteAsync(Guid id);
}
