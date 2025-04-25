using FS.Framework.Product.Application.Features.Product.Commands.CreateProduct;
using FS.Framework.Product.Application.Features.Product.Commands.DeleteProduct;
using FS.Framework.Product.Application.Features.Product.Commands.UpdateProduct;
using FS.Framework.Product.Application.Features.Product.Queries.GetAllProduct;
using FS.Framework.Product.Application.Features.Product.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.Framework.Product.Api.Controllers;

/// <summary>
/// Controlador para operaciones sobre Product (CRUD).
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los Productos registrados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllProductQuery()));


    /// <summary>
    /// Obtiene un Productos por su ID.
    /// </summary>
    /// <param name="id">Identificador del Producto.</param>
    /// <returns>Productos encontrado o 404 si no existe.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return product is null ? NotFound() : Ok(product);
    }

    /// <summary>
    /// Crea un nuevo roducto.
    /// </summary>
    /// <param name="command">Datos del roducto a crear.</param>
    /// <returns>roducto creado</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza un roducto existente.
    /// </summary>
    /// <param name="id">ID del roducto a actualizar.</param>
    /// <param name="command">Datos actualizados del roducto.</param>
    /// <returns>204 si fue exitoso o 400 si hay inconsistencia en el ID.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest();
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Elimina un roducto por ID (soft delete).
    /// </summary>
    /// <param name="id">ID del roducto a eliminar.</param>
    /// <returns>204 si fue eliminado.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
