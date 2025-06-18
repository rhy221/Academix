using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class ChucNang
    {
        public string MaCN { get; set; }
        public string TenCN { get; set; }
        public string TenManHinhDuocLoad { get; set; }


        public ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}
