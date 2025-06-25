using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class CtBctongkethocky
    {
        public CtBctongkethocky()
        {

        }

        public CtBctongkethocky(string mabctkhocky, string malop, int siso, int soluongdat, double tiledat)
        {
            Mabctkhocky = mabctkhocky;
            Malop = malop;
            Siso = siso;
            Soluongdat = soluongdat;
            Tiledat = tiledat;
           
        }
    }
}
