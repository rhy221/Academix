using Academix.Models;
using Academix.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Academix.ViewModels
{
    public class LoginWindowViewModel : ObservableObject
    {
        private string tenDangNhap;
        public string TenDangNhap
        {
            get => tenDangNhap;
            set => SetProperty(ref tenDangNhap, value);
        }

        private string matKhau;
        public string MatKhau
        {
            get => matKhau;
            set => SetProperty(ref matKhau, value);
        }
        public Action? CloseAction { get; set; }
        public ObservableCollection<NguoiDung> TaiKhoanAo { get; }

        public static string CurrentUserGroup { get; private set; }

        public IRelayCommand DangNhapCommand { get; }

        public LoginWindowViewModel()
        {
            TaiKhoanAo = new ObservableCollection<NguoiDung>
            {
                new NguoiDung { TenDangNhap = "admin01", MatKhau = "123456", MaNhom = "admin" },
                new NguoiDung { TenDangNhap = "hieutruong", MatKhau = "123456", MaNhom = "bgh" },
                new NguoiDung { TenDangNhap = "giaovu", MatKhau = "123456", MaNhom = "giaovu" }
            };

            DangNhapCommand = new RelayCommand(ThucHienDangNhap);
        }

        private void ThucHienDangNhap()
        {
            var user = TaiKhoanAo.FirstOrDefault(u => u.TenDangNhap == TenDangNhap && u.MatKhau == MatKhau);
            if (user != null)
            {
                CurrentUserGroup = user.MaNhom;

                Window? nextWindow = null;

                switch (CurrentUserGroup)
                {
                    case "admin":
                        nextWindow = new AdminDashboardView();
                        break;

                    default:
                        nextWindow = new MainWindow();
                        break;
                }

                nextWindow?.Show();
                CloseAction?.Invoke();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
