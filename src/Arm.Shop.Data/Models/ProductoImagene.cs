using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class ProductoImagene
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
