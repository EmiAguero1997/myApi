using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myApi.Data;
using myApi.Models;

namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AlmacenController : ControllerBase
{
    private readonly MyApiContext _context;

    public AlmacenController(MyApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Almacen>>> Get()
    {
        return await _context.Almacen.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Almacen>> Get(int id)
    {
        var almacen = await _context.Almacen.FindAsync(id);
        if (almacen is null)
        {
            return NotFound();
        }

        return almacen;
    }

    [HttpPost]
    public async Task<ActionResult<Almacen>> Post(Almacen almacen)
    {
        almacen.Id = 0;
        _context.Almacen.Add(almacen);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = almacen.Id }, almacen);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Almacen almacen)
    {
        var existente = await _context.Almacen.FindAsync(id);
        if (existente is null)
        {
            return NotFound();
        }

        existente.Nombre = almacen.Nombre;
        existente.Direccion = almacen.Direccion;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var almacen = await _context.Almacen.FindAsync(id);
        if (almacen is null)
        {
            return NotFound();
        }

        _context.Almacen.Remove(almacen);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
