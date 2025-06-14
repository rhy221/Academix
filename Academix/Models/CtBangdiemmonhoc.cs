using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class CtBangdiemmonhoc
{
    public string Mactbdmh { get; set; } = null!;

    public string Mabdmh { get; set; } = null!;

    public string Mahs { get; set; } = null!;

    public double Dtbmon { get; set; }

    public virtual ICollection<CtDiemmonhoc> CtDiemmonhocs { get; set; } = new List<CtDiemmonhoc>();

    public virtual Bangdiemmonhoc MabdmhNavigation { get; set; } = null!;

    public virtual Hocsinh MahsNavigation { get; set; } = null!;
}
