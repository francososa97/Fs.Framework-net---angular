namespace FS.Framework.Product.Application.DTOs;

/// <summary>
/// Representa los datos de un producto para exposición en la API.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre de producto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del producto.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Indica si el producto fue marcado como eliminado (soft delete).
    /// </summary>
    public int stock { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
