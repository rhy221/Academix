using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Khoi
    {
        public bool IsAll = false;
        public Khoi()
        {

        }

        public override string ToString()
        {
            if (IsAll)
                return "[Tất cả]";
            return Tenkhoi;
        }
    }
}
