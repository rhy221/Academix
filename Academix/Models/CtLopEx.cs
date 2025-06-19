using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class CtLop
    {
        
        public CtLop()
        {

        }

        public CtLop(string malop, string mahs, string mahocky, double dtbhk)
        {
            this.Malop = malop;
            this.Mahs = mahs;
            this.Mahocky = mahocky;
            this.Dtbhk = dtbhk;
           
        }
    }

}
