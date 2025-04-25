using FS.Framework.Product.Domain.Entities;

/// <summary>
/// Repositorio encargado de la persistencia de Producto.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Obtiene todos los Producto que no están eliminados.
    /// </summary>
    Task<IEnumerable<ProductModel>> GetAllAsync();

    /// <summary>
    /// Obtiene un producto por su ID.
    /// </summary>
    Task<ProductModel> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo producto.
    /// </summary>
    Task<ProductModel> AddAsync(ProductModel product);

    /// <summary>
    /// Actualiza los datos de un producto.
    /// </summary>
    ProductModel Update(ProductModel product);

    /// <summary>
    /// Guarda los cambios pendientes.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Realiza un soft delete del producto.
    /// </summary>
    Task DeleteAsync(Guid id);
}
