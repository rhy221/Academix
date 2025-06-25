using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class CtDiemmonhoc
{
    public string Mactbdmh { get; set; } = null!;

    public string Maloaidiem { get; set; } = null!;

    public int Lan { get; set; }

    public double Diem { get; set; }

    public virtual CtBangdiemmonhoc MactbdmhNavigation { get; set; } = null!;

    public virtual Loaidiem MaloaidiemNavigation { get; set; } = null!;
}
