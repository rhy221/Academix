using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Academix.ViewModels
{
    public class PermissionMatrixRow
    {
        public string MaChucNang { get; set; }
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

        public IRelayCommand SaveMatrixCommand { get; }

        public PermissionMatrixViewModel()
        {
            SaveMatrixCommand = new RelayCommand(SaveMatrix);
            LoadFromDatabase();
            BuildMatrix();
        }

        private void LoadFromDatabase()
        {
            using var db = new PhanQuyenNguoiDungContext();
            groups = db.NhomNguoiDung.AsNoTracking().ToList();
            functions = db.ChucNang.AsNoTracking().ToList();
            permissions = db.PhanQuyen.AsNoTracking().ToList();
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
                    MaChucNang = f.MaCN,
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

        private void SaveMatrix()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var allPermissions = db.PhanQuyen.ToList();
            db.PhanQuyen.RemoveRange(allPermissions);

            foreach (var row in PermissionMatrix)
            {
                foreach (var entry in row.Permissions)
                {
                    var group = groups.FirstOrDefault(g => g.TenNhom == entry.Key);
                    if (group != null && entry.Value)
                    {
                        db.PhanQuyen.Add(new PhanQuyen
                        {
                            MaNhom = group.MaNhom,
                            MaChucNang = row.MaChucNang
                        });
                    }
                }
            }

            db.SaveChanges();
            System.Windows.MessageBox.Show("Đã lưu phân quyền vào CSDL!", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
    }
}
