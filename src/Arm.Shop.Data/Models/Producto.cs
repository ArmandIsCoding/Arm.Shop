using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaAlta { get; set; }

    public virtual ICollection<ProductoVariacione> ProductoVariaciones { get; set; } = new List<ProductoVariacione>();
}
