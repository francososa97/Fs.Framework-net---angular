using FS.Framework.Product.Domain.Entities;

/// <summary>
/// Repositorio encargado de la persistencia de usuarios.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Obtiene todos los usuarios que no están eliminados.
    /// </summary>
    Task<IEnumerable<ProductModel>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    Task<ProductModel> GetByIdAsync(Guid id);

    /// <summary>
    /// Agrega un nuevo usuario.
    /// </summary>
    Task<ProductModel> AddAsync(ProductModel product);

    /// <summary>
    /// Actualiza los datos de un usuario.
    /// </summary>
    ProductModel Update(ProductModel product);

    /// <summary>
    /// Guarda los cambios pendientes.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Realiza un soft delete del usuario.
    /// </summary>
    Task DeleteAsync(Guid id);
}
