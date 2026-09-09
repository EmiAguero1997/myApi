namespace myApi.Models;

public class Almacen
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;
    public ICollection<AlmacenProducto> AlmacenProductos { get; set; } = new List<AlmacenProducto>();
}
