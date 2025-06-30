using System;
using System.Collections.Generic;

namespace Academix.Models;

public partial class Hocsinh
{
    public string Mahs { get; set; } = null!;

    public string Hoten { get; set; } = null!;

    public string Gioitinh { get; set; } = null!;

    public DateTime Ngaysinh { get; set; }

    public string Diachi { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<CtBangdiemmonhoc> CtBangdiemmonhocs { get; set; } = new List<CtBangdiemmonhoc>();

    public virtual ICollection<CtLop> CtLops { get; set; } = new List<CtLop>();
}
