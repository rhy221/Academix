using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class CtDiemmonhoc
    {
       

        public CtDiemmonhoc()
        {

        }

        public CtDiemmonhoc(string maCtbdmh, string maLoaiDiem, int lan, double diem)
        {
            Mactbdmh = maCtbdmh;
            Maloaidiem = maLoaiDiem;
            Lan = lan;
            Diem = diem;
        }
    }
}
