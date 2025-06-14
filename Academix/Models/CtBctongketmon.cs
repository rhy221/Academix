using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class CtBctongketmon
{
    public string Mabctkmon { get; set; } = null!;

    public string Malop { get; set; } = null!;

    public int Siso { get; set; }

    public int Soluongdat { get; set; }

    public double Tiledat { get; set; }

    public virtual Bctongketmon MabctkmonNavigation { get; set; } = null!;

    public virtual Lop MalopNavigation { get; set; } = null!;
}
