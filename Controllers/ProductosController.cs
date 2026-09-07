using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myApi.Data;
using myApi.Models;

namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private readonly MyApiContext _context;

    public ProductosController(MyApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> Get()
    {
        return await _context.Productos.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Producto>> Get(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null)
        {
            return NotFound();
        }

        return producto;
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> Post(Producto producto)
    {
        producto.Id = 0;
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Producto producto)
    {
        var existente = await _context.Productos.FindAsync(id);
        if (existente is null)
        {
            return NotFound();
        }

        existente.Nombre = producto.Nombre;
        existente.UnidadDeMedida = producto.UnidadDeMedida;
        existente.Cantidad = producto.Cantidad;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null)
        {
            return NotFound();
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
