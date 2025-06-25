using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Bctongketmon
{
    public string Mabctkmon { get; set; } = null!;

    public string Mamh { get; set; } = null!;

    public string Mahocky { get; set; } = null!;

    public string Manamhoc { get; set; } = null!;

    public virtual ICollection<CtBctongketmon> CtBctongketmons { get; set; } = new List<CtBctongketmon>();

    public virtual Hocky MahockyNavigation { get; set; } = null!;

    public virtual Monhoc MamhNavigation { get; set; } = null!;

    public virtual Namhoc ManamhocNavigation { get; set; } = null!;
}
