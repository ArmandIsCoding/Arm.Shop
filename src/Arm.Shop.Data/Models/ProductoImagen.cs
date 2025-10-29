namespace Arm.Shop.Data.Models
{
    public class ProductoImagen
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
        public bool EsPrincipal { get; set; }

        public Producto Producto { get; set; } = null!;
    }
}
