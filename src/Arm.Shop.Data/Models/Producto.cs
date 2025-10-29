namespace Arm.Shop.Data.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual ICollection<ProductoVariacione> ProductoVariaciones { get; set; } = [];

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; } = null!;

    // Nueva navegación
    public virtual ICollection<ProductoImagen> Imagenes { get; set; } = new List<ProductoImagen>();
}
