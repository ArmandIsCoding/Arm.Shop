namespace Arm.Shop.Core.DTOs
{
    public class ProductoVariacionDto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        // Ejemplo: "Talle M - Rojo"
        public string? Descripcion { get; set; }
    }
}
