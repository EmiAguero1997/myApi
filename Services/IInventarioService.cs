public interface IInventarioService
{
    Task<IEnumerable<ProductoInventarioDto>> ObtenerProductosPorAlmacenAsync(int almacenId);
    Task<IEnumerable<ProductoInventarioDto>> ObtenerTodosLosProductos();
    Task<bool> AsignarOActualizarStockAsync(AgregarStockDto dto);
}