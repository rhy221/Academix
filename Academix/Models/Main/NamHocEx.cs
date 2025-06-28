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
        public Namhoc()
        {

        }
        public Namhoc(string maNH, int nam1, int nam2)
        {
            Manamhoc = maNH;
            Nam1 = nam1;
            Nam2 = nam2;
        }
        public override string ToString()
        {
            if (IsAll)
                return "[Tất cả]";

            return $"{Nam1}-{Nam2}";
        }
    }
}
