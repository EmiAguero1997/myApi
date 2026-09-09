public class AgregarStockDto
{
    public int ProductoId { get; set; }
    public int AlmacenId { get; set; }
}

public class ProductoInventarioDto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal UnidadDeMedida { get; set; }
    public int Cantidad { get; set; }
}