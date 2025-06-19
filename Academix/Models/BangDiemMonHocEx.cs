using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Bangdiemmonhoc
    {
       public Bangdiemmonhoc()
        {

        }

        public Bangdiemmonhoc(string maBdMh, string maLop, string maMh, string maHk)
        {
            Mabdmh = maBdMh;
            Malop = maLop;
            Mamh = maMh;
            Mahocky = maHk;
        }
    }

}
