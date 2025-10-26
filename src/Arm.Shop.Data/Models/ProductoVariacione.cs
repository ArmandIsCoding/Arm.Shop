using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class ProductoVariacione
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string Sku { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual ICollection<AtributoValore> AtributoValors { get; set; } = new List<AtributoValore>();
}
