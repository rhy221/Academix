using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Bctongkethocky
    {
       

        public Bctongkethocky()
        {

        }
        public Bctongkethocky(string mabctkhocky, string mahocky, string manamhoc)
        {
            this.Mabctkhocky = mabctkhocky;
            this.Mahocky = mahocky;
            this.Manamhoc = manamhoc;
        }
    }
}
