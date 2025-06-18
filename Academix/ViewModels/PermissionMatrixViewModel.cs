using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace Academix.ViewModels
{
    public class PermissionMatrixRow
    {
        public string TenChucNang { get; set; }
        public Dictionary<string, bool> Permissions { get; set; } = new();
    }

    public partial class PermissionMatrixViewModel : ObservableObject
    {
        public ObservableCollection<PermissionMatrixRow> PermissionMatrix { get; set; } = new();
        public ObservableCollection<string> GroupHeaders { get; set; } = new();

        private List<NhomNguoiDung> groups;
        private List<ChucNang> functions;
        private List<PhanQuyen> permissions;

        public PermissionMatrixViewModel()
        {
            LoadFakeData();
            BuildMatrix();
        }

        private void LoadFakeData()
        {
            groups = new List<NhomNguoiDung>
            {
                new NhomNguoiDung { MaNhom = "admin", TenNhom = "Admin" },
                new NhomNguoiDung { MaNhom = "bgh", TenNhom = "Ban Giám hiệu" },
                new NhomNguoiDung { MaNhom = "giaovu", TenNhom = "Giáo vụ" },
            };

            functions = new List<ChucNang>
            {
                new ChucNang { MaCN = "CN01", TenCN = "Quản lý môn học", TenManHinhDuocLoad = "SubjectReportView" },
                new ChucNang { MaCN = "CN02", TenCN = "Quản lý tài khoản", TenManHinhDuocLoad = "AccountManageView" },
                new ChucNang { MaCN = "CN03", TenCN = "Phân quyền", TenManHinhDuocLoad = "PermissionManageView" }
            };

            permissions = new List<PhanQuyen>
            {
                new PhanQuyen { MaNhom = "admin", MaChucNang = "CN01" },
                new PhanQuyen { MaNhom = "admin", MaChucNang = "CN02" },
                new PhanQuyen { MaNhom = "admin", MaChucNang = "CN03" },
                new PhanQuyen { MaNhom = "bgh", MaChucNang = "CN01" },
                new PhanQuyen { MaNhom = "giaovu", MaChucNang = "CN02" }
            };
        }

        private void BuildMatrix()
        {
            GroupHeaders.Clear();
            foreach (var g in groups)
                GroupHeaders.Add(g.TenNhom);

            PermissionMatrix.Clear();
            foreach (var f in functions)
            {
                var row = new PermissionMatrixRow
                {
                    TenChucNang = f.TenCN
                };

                foreach (var g in groups)
                {
                    bool hasPermission = permissions.Any(p => p.MaNhom == g.MaNhom && p.MaChucNang == f.MaCN);
                    row.Permissions[g.TenNhom] = hasPermission;
                }

                PermissionMatrix.Add(row);
            }
        }
    }
}
