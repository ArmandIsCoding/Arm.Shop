using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class OrdenItem
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public int VariacionId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Ordene Orden { get; set; } = null!;

    public virtual ProductoVariacione Variacion { get; set; } = null!;
}
