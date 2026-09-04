using Microsoft.AspNetCore.Mvc;
using myApi.Models;

namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private static readonly List<Producto> Productos =
    [
        new() { Id = 1, Nombre = "Harina", UnidadDeMedida = "kg", Cantidad = 10 },
        new() { Id = 2, Nombre = "Leche", UnidadDeMedida = "litro", Cantidad = 5 }
    ];

    private static int _nextId = 3;

    [HttpGet]
    public ActionResult<IEnumerable<Producto>> Get() => Productos;

    [HttpGet("{id:int}")]
    public ActionResult<Producto> Get(int id)
    {
        var producto = Productos.FirstOrDefault(p => p.Id == id);
        if (producto is null)
        {
            return NotFound();
        }

        return producto;
    }

    [HttpPost]
    public ActionResult<Producto> Post(Producto producto)
    {
        producto.Id = _nextId++;
        Productos.Add(producto);
        return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Producto producto)
    {
        var existente = Productos.FirstOrDefault(p => p.Id == id);
        if (existente is null)
        {
            return NotFound();
        }

        existente.Nombre = producto.Nombre;
        existente.UnidadDeMedida = producto.UnidadDeMedida;
        existente.Cantidad = producto.Cantidad;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var producto = Productos.FirstOrDefault(p => p.Id == id);
        if (producto is null)
        {
            return NotFound();
        }

        Productos.Remove(producto);
        return NoContent();
    }
}
