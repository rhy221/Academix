using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("NGUOIDUNG")]
    public class NguoiDung
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public string MaNhom { get; set; }


        public NhomNguoiDung NhomNguoiDung { get; set; }
    }
}
