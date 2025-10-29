using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
