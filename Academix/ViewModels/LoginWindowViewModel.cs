using Academix.Helpers;
using Academix.Models;
using Academix.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using Academix.Helpers;

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

        public static string CurrentUserGroup { get; private set; }

        public IRelayCommand DangNhapCommand { get; }

        public LoginWindowViewModel()
        {
            DangNhapCommand = new RelayCommand(ThucHienDangNhap);
        }

        private void ThucHienDangNhap()
        {
            using (var db = new PhanQuyenNguoiDungContext())
            {
                var user = db.NguoiDung
                             .Include(u => u.NhomNguoiDung)
                             .FirstOrDefault(u => u.TenDangNhap == TenDangNhap && u.MatKhau == MatKhau);

                if (user != null)
                {
                    CurrentUserGroup = user.MaNhom;

                    Session.TenDangNhap = user.TenDangNhap;
                    Session.MaNhom = user.MaNhom;
                    Session.TenNhom = user.NhomNguoiDung?.TenNhom;


                    Session.ManHinhDuocLoadDuocPhep = db.PhanQuyen
                        .Include(p => p.ChucNang)
                        .Where(p => p.MaNhom == user.MaNhom)
                        .Select(p => p.ChucNang.TenManHinhDuocLoad)
                        .Distinct()
                        .ToList();

                    Window? nextWindow = CurrentUserGroup == "ADMIN"
                        ? new AdminDashboardView()
                        : new MainWindow();

                    nextWindow.Show();
                    CloseAction?.Invoke();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
