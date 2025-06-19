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
using Academix.Stores;
using System.Windows.Input;

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

        public ICommand DangNhapCommand { get; }

        public LoginWindowViewModel()
        {
            DangNhapCommand = new AsyncRelayCommand(ThucHienDangNhap);
        }

        private async Task ThucHienDangNhap()
        { 
            try
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

                        if (CurrentUserGroup == "ADMIN")
                        {
                            new AdminDashboardView().Show();
                        }
                        else
                        {
                            NavigationStore _navigationStore = new NavigationStore();
                            SchoolYearStore _schoolYearStore = new SchoolYearStore();

                            List<Namhoc> schoolYears;

                            using (var context = new QuanlyhocsinhContext())
                            {
                                schoolYears = await context.Namhocs.ToListAsync();
                            }

                            Namhoc allSchoolYear = new Namhoc();
                            allSchoolYear.IsAll = true;
                            schoolYears.Insert(0, allSchoolYear);
                            _schoolYearStore.SchoolYears = schoolYears;
                            _schoolYearStore.SelectedSchoolYear = schoolYears[schoolYears.Count - 1];


                            Window mainWindow = new MainWindow();
                            mainWindow.DataContext = new MainViewModel(_navigationStore, _schoolYearStore, mainWindow);
                            mainWindow.Show();

                        }



                        CloseAction?.Invoke();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }

    }
}
