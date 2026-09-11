namespace myApi.Models;

public class AlmacenProducto
{
    public int AlmacenId { get; set; }
    public Almacen Almacen { get; set; } = null!;
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
    public string Fecha { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
}