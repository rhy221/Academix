using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Lop
{
    public string Malop { get; set; } = null!;

    public string Tenlop { get; set; } = null!;

    public int Siso { get; set; }

    public string Makhoi { get; set; } = null!;

    public string Manamhoc { get; set; } = null!;

    public virtual ICollection<Bangdiemmonhoc> Bangdiemmonhocs { get; set; } = new List<Bangdiemmonhoc>();

    public virtual ICollection<CtBctongkethocky> CtBctongkethockies { get; set; } = new List<CtBctongkethocky>();

    public virtual ICollection<CtBctongketmon> CtBctongketmons { get; set; } = new List<CtBctongketmon>();

    public virtual ICollection<CtLop> CtLops { get; set; } = new List<CtLop>();

    public virtual Khoi MakhoiNavigation { get; set; } = null!;

    public virtual Namhoc ManamhocNavigation { get; set; } = null!;
}
