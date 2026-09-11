using Microsoft.EntityFrameworkCore;
using myApi.Data;
using myApi.Models;

public class InventarioService : IInventarioService
{
    private readonly MyApiContext _context;

    public InventarioService(MyApiContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ProductoInventarioDto>> ObtenerTodosLosProductos()
    {
        var productos = await _context.Productos
            .Select(p => new ProductoInventarioDto
            {
                ProductoId = p.Id,
                Nombre = p.Nombre,
                UnidadDeMedida = p.UnidadDeMedida,
                Cantidad = _context.AlmacenProducto
                    .Where(ap => ap.ProductoId == p.Id)
                    .Sum(ap => (decimal?)ap.Cantidad) ?? 0 // Sumar la cantidad de todos los almacenes para este producto
            })
            .ToListAsync();

        return productos;
    }
    public async Task<IEnumerable<ProductoInventarioDto>> ObtenerProductosPorAlmacenAsync(int almacenId)
    {
        var productos = await _context.AlmacenProducto
            .Where(ap => ap.AlmacenId == almacenId)
            .Select(ap => new ProductoInventarioDto
            {
                ProductoId = ap.ProductoId,
                Nombre = ap.Producto.Nombre,
                UnidadDeMedida = ap.Producto.UnidadDeMedida,
                Cantidad = ap.Cantidad
            })
            .ToListAsync();

        return productos;
    }

    public async Task<bool> AsignarOActualizarStockAsync( AgregarStockDto dto)
    {
        var almacenProducto = await _context.AlmacenProducto
            .FirstOrDefaultAsync(ap => ap.AlmacenId == dto.AlmacenId && ap.ProductoId == dto.ProductoId);

        if (almacenProducto != null)
        {
            // Actualizar stock existente
            almacenProducto.Cantidad += dto.Cantidad;
        }
        else
        {
            // Crear nuevo registro de stock
            almacenProducto = new AlmacenProducto
            {
                AlmacenId = dto.AlmacenId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad
            };
            _context.AlmacenProducto.Add(almacenProducto);
        }

        await _context.SaveChangesAsync();
        return true;
    }
}