using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Lop
    {
        public Lop()
        {

        }

        public Lop(string maLop, string tenLop ,int siSo, string maKhoi, string maNam)
        {
            Malop = maLop;
            Tenlop = tenLop;
            Siso = siSo;
            Makhoi = maKhoi;
            Manamhoc = maNam;
        }
    }
}
