using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class CtLop
{
    public string Malop { get; set; } = null!;

    public string Mahs { get; set; } = null!;

    public string Mahocky { get; set; } = null!;

    public double Dtbhk { get; set; }

    public virtual Hocky MahockyNavigation { get; set; } = null!;

    public virtual Hocsinh MahsNavigation { get; set; } = null!;

    public virtual Lop MalopNavigation { get; set; } = null!;
}
