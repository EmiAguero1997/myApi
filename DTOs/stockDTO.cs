public class AgregarStockDto
{
    public int ProductoId { get; set; }
    public int AlmacenId { get; set; }
    public decimal Cantidad { get; set; }
}

public class ProductoInventarioDto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string UnidadDeMedida { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
}