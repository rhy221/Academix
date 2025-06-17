using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Namhoc
    {
        public bool IsAll = false;
        public override string ToString()
        {
            if (IsAll)
                return "[Tất cả]";

            return $"{Nam1}-{Nam2}";
        }
    }
}
