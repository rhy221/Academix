using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class CtBctongketmon
    {
        public CtBctongketmon()
        {

        }
        public CtBctongketmon(string mabctkmon, string malop, int siso, int soluongdat, double tiledat)
        {
            this.Mabctkmon = mabctkmon;
            this.Malop = malop;
            this.Siso = siso;
            this.Soluongdat = soluongdat;
            this.Tiledat = tiledat;         
        }
    }

}
