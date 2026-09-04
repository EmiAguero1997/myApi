namespace myApi.Models;

public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string UnidadDeMedida { get; set; } = string.Empty;

    public decimal Cantidad { get; set; }
}
