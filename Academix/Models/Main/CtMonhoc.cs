using System;
using System.Collections.Generic;

namespace Academix.Models.Main;

public partial class CtMonhoc
{
    public string Mamh { get; set; } = null!;

    public string Maloaidiem { get; set; } = null!;

    public int Socot { get; set; }

    public virtual Loaidiem MaloaidiemNavigation { get; set; } = null!;

    public virtual Monhoc MamhNavigation { get; set; } = null!;
}
