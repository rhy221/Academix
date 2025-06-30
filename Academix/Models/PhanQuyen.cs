using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("PHANQUYEN")]
    public class PhanQuyen
    {
        public string MaNhom { get; set; }
        public string MaChucNang { get; set; }


        public NhomNguoiDung NhomNguoiDung { get; set; }
        public ChucNang ChucNang { get; set; }
    }
}
