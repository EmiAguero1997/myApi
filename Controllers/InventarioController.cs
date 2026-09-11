using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myApi.Data;
using myApi.Models;

namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InventarioController : ControllerBase
{
    private readonly IInventarioService _inventarioService;

    public InventarioController(IInventarioService inventarioService)
    {
        _inventarioService = inventarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoInventarioDto>>> ObtenerTodosLosProductos()
    {
        var productos = await _inventarioService.ObtenerTodosLosProductos(); // 0 para obtener todos los productos
        return Ok(productos);
    }

    [HttpGet ("{almacenId:int}")]
    public async Task<ActionResult<IEnumerable<ProductoInventarioDto>>> ObtenerProductosPorAlmacen([FromRoute] int almacenId)
    {
        var productos = await _inventarioService.ObtenerProductosPorAlmacenAsync(almacenId);
        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult> AsignarOActualizarStock(AgregarStockDto dto)
    {
        var resultado = await _inventarioService.AsignarOActualizarStockAsync(dto);
        if (resultado)
        {
            return Ok();
        }
        else
        {
            return BadRequest("No se pudo asignar o actualizar el stock.");
        }
    }
}
