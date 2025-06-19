using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class CtBangdiemmonhoc
    {
        
       public CtBangdiemmonhoc()
        {

        }

        public CtBangdiemmonhoc(string maCtbdmh, string maBdmh, string maHs, double dTbmon)
        {
            Mactbdmh = maCtbdmh;
            Mabdmh = maBdmh;
            Mahs = maHs;
            Dtbmon = dTbmon;
        }
    }

}
