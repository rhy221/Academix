using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Bctongkethocky
{
    public string Mabctkhocky { get; set; } = null!;

    public string Mahocky { get; set; } = null!;

    public string Manamhoc { get; set; } = null!;

    public virtual ICollection<CtBctongkethocky> CtBctongkethockies { get; set; } = new List<CtBctongkethocky>();

    public virtual Hocky MahockyNavigation { get; set; } = null!;

    public virtual Namhoc ManamhocNavigation { get; set; } = null!;
}
