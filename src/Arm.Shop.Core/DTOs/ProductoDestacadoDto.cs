namespace Arm.Shop.Core.DTOs
{
    public class ProductoDestacadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = "/imagenes/productos/sample.jpg"; // fallback
        public int Reviews { get; set; } = 0; // opcional
    }
}
