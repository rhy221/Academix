using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Monhoc
{
    public string Mamh { get; set; } = null!;

    public string Tenmh { get; set; } = null!;

    public int Heso { get; set; }

    public virtual ICollection<Bangdiemmonhoc> Bangdiemmonhocs { get; set; } = new List<Bangdiemmonhoc>();

    public virtual ICollection<Bctongketmon> Bctongketmons { get; set; } = new List<Bctongketmon>();

    public virtual ICollection<Loaidiem> Maloaidiems { get; set; } = new List<Loaidiem>();
}
