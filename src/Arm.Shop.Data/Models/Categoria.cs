using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? CategoriaPadreId { get; set; }

    public string? ImagenNombreArchivo { get; set; }

    public virtual Categoria? CategoriaPadre { get; set; }

    public virtual ICollection<Categoria> InverseCategoriaPadre { get; set; } = new List<Categoria>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
