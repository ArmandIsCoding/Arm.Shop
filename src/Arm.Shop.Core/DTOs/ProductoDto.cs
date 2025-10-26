namespace Arm.Shop.Core.DTOs
{
    // DTOs suelen ser públicos para que puedan viajar entre capas
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public int? Stock { get; set; }

        // Se puede incluir una lista de variaciones para mostrar talles/colores/etc
        public List<ProductoVariacionDto> Variaciones { get; set; } = [];
    }
}