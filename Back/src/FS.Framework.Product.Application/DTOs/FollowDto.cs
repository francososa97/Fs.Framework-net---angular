namespace FS.FakeTwitter.Application.DTOs;

/// <summary>
/// Representa una relación de seguimiento entre Producto (quién sigue a quién).
/// </summary>
public class FollowDto
{
    /// <summary>
    /// ID del producto que sigue.
    /// </summary>
    public string FollowerId { get; set; } = string.Empty;

    /// <summary>
    /// ID del producto al que se sigue.
    /// </summary>
    public string FolloweeId { get; set; } = string.Empty;
}
