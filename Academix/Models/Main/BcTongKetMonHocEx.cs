using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Bctongketmon
    {
        public Bctongketmon()
        {

        }
        public Bctongketmon(string maBCTKM, string mMH, string mHK, string mNH)
        {
            Mabctkmon = maBCTKM;
            Mamh = mMH;
            Mahocky = mHK;
            Manamhoc = mNH;
        }
    }
}
