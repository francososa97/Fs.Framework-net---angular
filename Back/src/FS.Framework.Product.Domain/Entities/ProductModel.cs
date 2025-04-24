namespace FS.Framework.Product.Domain.Entities;

public class ProductModel
{
    /// <summary>
    /// Identificador único del producto.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Precio del producto.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Stock disponible.
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Fecha de creación.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Fecha de última modificación.
    /// </summary>
    public DateTime Updated { get; set; }
    public bool IsDeleted { get; set; }
}
