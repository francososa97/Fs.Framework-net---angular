namespace FS.Framework.Product.Application.DTOs;

/// <summary>
/// Representa los datos de un usuario para exposición en la API.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre de usuario.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Indica si el usuario fue marcado como eliminado (soft delete).
    /// </summary>
    public int stock { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
