using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public partial class Hocsinh
    {
        
        public Hocsinh()
        {

        }

        public Hocsinh(string maHs, string hoTen, string gioiTinh, DateTime ngaySinh, string diaChi, string email)
        {
            Mahs = maHs;
            Hoten = hoTen;
            Gioitinh = gioiTinh;
            Ngaysinh = ngaySinh;
            Diachi = diaChi;
            Email = email;
        }
    }


}
