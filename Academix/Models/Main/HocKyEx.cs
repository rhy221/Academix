using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{

    public partial class Hocky
    {

        public Hocky()
        {

        }

        public Hocky(string maHk, string tenHK)
        {
            Mahocky = maHk;
            Tenhocky = tenHK;
        }
        public override string ToString()
        {
            return Tenhocky;
        }
    }

}
