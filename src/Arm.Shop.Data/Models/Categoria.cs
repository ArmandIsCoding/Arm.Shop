namespace Arm.Shop.Data.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int? CategoriaPadreId { get; set; }

        // 🔁 Navegación recursiva
        public Categoria? CategoriaPadre { get; set; }

        public List<Categoria> Subcategorias { get; set; } = new();

        // 🔗 Productos asociados
        public List<Producto> Productos { get; set; } = new();
    }
}