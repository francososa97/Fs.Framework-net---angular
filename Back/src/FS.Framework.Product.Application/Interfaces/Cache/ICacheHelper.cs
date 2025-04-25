using FS.Framework.Product.Domain.Entities;

namespace FS.Framework.Product.Application.Interfaces.Cache;

/// <summary>
/// Abstracción para manejar operaciones de caching relacionadas con relaciones de seguidores y timelines.
/// </summary>
public interface ICacheHelper
{
    /// <summary>
    /// Obtiene de caché o ejecuta una función para recuperar la lista completa de productos.
    /// </summary>
    Task<IEnumerable<ProductModel>> GetOrSetProductsAsync(Func<Task<IEnumerable<ProductModel>>> factory);
}
