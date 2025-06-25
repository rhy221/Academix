using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Bangdiemmonhoc
{
    public string Mabdmh { get; set; } = null!;

    public string Malop { get; set; } = null!;

    public string Mamh { get; set; } = null!;

    public string Mahocky { get; set; } = null!;

    public virtual ICollection<CtBangdiemmonhoc> CtBangdiemmonhocs { get; set; } = new List<CtBangdiemmonhoc>();

    public virtual Hocky MahockyNavigation { get; set; } = null!;

    public virtual Lop MalopNavigation { get; set; } = null!;

    public virtual Monhoc MamhNavigation { get; set; } = null!;
}
