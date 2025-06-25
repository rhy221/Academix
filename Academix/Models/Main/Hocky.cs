using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Hocky
{
    public string Mahocky { get; set; } = null!;

    public string Tenhocky { get; set; } = null!;

    public virtual ICollection<Bangdiemmonhoc> Bangdiemmonhocs { get; set; } = new List<Bangdiemmonhoc>();

    public virtual ICollection<Bctongkethocky> Bctongkethockies { get; set; } = new List<Bctongkethocky>();

    public virtual ICollection<Bctongketmon> Bctongketmons { get; set; } = new List<Bctongketmon>();

    public virtual ICollection<CtLop> CtLops { get; set; } = new List<CtLop>();
}
