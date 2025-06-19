using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Helpers
{
    public static class Session
    {
        public static string TenDangNhap { get; set; }
        public static string MaNhom { get; set; }
        public static string TenNhom { get; set; }

        public static List<string> ManHinhDuocLoadDuocPhep { get; set; } = new();

        public static void Clear()
        {
            TenDangNhap = null;
            MaNhom = null;
            TenNhom = null;
            ManHinhDuocLoadDuocPhep.Clear();
        }
    }
}
