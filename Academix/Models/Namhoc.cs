using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Namhoc
{
    public string Manamhoc { get; set; } = null!;

    public int Nam1 { get; set; }

    public int Nam2 { get; set; }

    public virtual ICollection<Bctongkethocky> Bctongkethockies { get; set; } = new List<Bctongkethocky>();

    public virtual ICollection<Bctongketmon> Bctongketmons { get; set; } = new List<Bctongketmon>();

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
