using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class Orden
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();

    public virtual Usuario Usuario { get; set; } = null!;
}
