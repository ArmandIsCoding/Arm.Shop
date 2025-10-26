using System;
using System.Collections.Generic;

namespace Arm.Shop.Data.Models;

public partial class Atributo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<AtributoValore> AtributoValores { get; set; } = new List<AtributoValore>();
}
