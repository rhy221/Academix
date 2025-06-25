using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Khoi
{
    public string Makhoi { get; set; } = null!;

    public string Tenkhoi { get; set; } = null!;

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
