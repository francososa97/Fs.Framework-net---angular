/// <summary>
/// Interface para coordinar la persistencia de múltiples repositorios.
/// </summary>
public interface IUnitOfWork
{

    /// <summary>
    /// Repositorio de products.
    /// </summary>
    IProductRepository Product { get; }

    /// <summary>
    /// Guarda los cambios realizados en las entidades persistidas.
    /// </summary>
    Task<int> SaveChangesAsync();
}
