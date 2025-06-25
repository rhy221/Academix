using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Loaidiem
{
    public string Maloaidiem { get; set; } = null!;

    public string Tenloaidiem { get; set; } = null!;

    public int Hesold { get; set; }

    public virtual ICollection<CtDiemmonhoc> CtDiemmonhocs { get; set; } = new List<CtDiemmonhoc>();

    public virtual ICollection<Monhoc> Mamhs { get; set; } = new List<Monhoc>();
}
