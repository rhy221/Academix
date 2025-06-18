using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class NguoiDung
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public string MaNhom { get; set; }


        public NhomNguoiDung NhomNguoiDung { get; set; }
    }
}
