using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("NHOMNGUOIDUNG")]
    public class NhomNguoiDung
    {
        public string MaNhom { get; set; }
        public string TenNhom { get; set; }


        public ICollection<PhanQuyen> PhanQuyens { get; set; }
        public ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
