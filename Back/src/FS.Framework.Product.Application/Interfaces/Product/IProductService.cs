using FS.Framework.Product.Domain.Entities;

/// <summary>
/// Servicio de aplicación para la gestión de usuarios.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Obtiene todos los usuarios activos.
    /// </summary>
    Task<IEnumerable<ProductModel>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por ID.
    /// </summary>
    Task<ProductModel?> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo usuario.
    /// </summary>
    Task<ProductModel> AddAsync(ProductModel product);

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    Task<ProductModel> UpdateAsync(ProductModel product);

    /// <summary>
    /// Marca un usuario como eliminado.
    /// </summary>
    Task<int> DeleteAsync(Guid id);
}
