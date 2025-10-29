namespace Arm.Shop.Core.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = "/imagenes/categorias/sample.jpg";
    }
}
