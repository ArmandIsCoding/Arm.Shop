using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class AtributoValore
{
    public int Id { get; set; }

    public int AtributoId { get; set; }

    public string Valor { get; set; } = null!;

    public virtual Atributo Atributo { get; set; } = null!;

    public virtual ICollection<ProductoVariacione> Variacions { get; set; } = new List<ProductoVariacione>();
}
