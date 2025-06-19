using Academix.Helpers;
using Academix.Models;
using Academix.Services;
using Academix.Stores;
using Academix.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class HamburgerMenuViewModel:BaseViewModel
    {

        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
        private Window _mainWindow;
        public ICommand StudentNavigateCommand { get; }
        public ICommand ClassNavigateCommand { get; }
        public ICommand GradeNavigateCommand { get; }
        public ICommand ReportNavigateCommand { get; }
        public ICommand DataSystemNavigateCommand { get; }
        public ICommand LogOutCommand { get; }

        public HamburgerMenuViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore, Window mainWindow)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _mainWindow = mainWindow;
            StudentNavigateCommand = new RelayCommand(StudentNavigate);
            ClassNavigateCommand = new RelayCommand(ClassNaviget);
            GradeNavigateCommand = new RelayCommand(GradeNavigate);
            ReportNavigateCommand = new RelayCommand(ReportNavigate);
            DataSystemNavigateCommand = new RelayCommand(DataSystemNavigate);
            LogOutCommand = new RelayCommand(LogOut);

            _navigationService.Navigate(new StudentsViewModel(_navigationService, _schoolYearStore));
        }

        private void StudentNavigate()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == "Học sinh");
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            _navigationService.Navigate(new StudentsViewModel(_navigationService, _schoolYearStore));
        }

        private void ClassNaviget()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == "Lớp");
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _navigationService.Navigate(new ClassesViewModel(_navigationService, _schoolYearStore));

        }

        private void GradeNavigate()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == "Điểm");
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _navigationService.Navigate(new GradeViewModel(_navigationService, _schoolYearStore) );

        }

        private void ReportNavigate()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == "Báo cáo");
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _navigationService.Navigate(new ReportViewModel(_navigationService, _schoolYearStore) );

        }

        private void DataSystemNavigate()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == "Hệ thống");
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _navigationService.Navigate(new DataSystemViewModel(_navigationService, _schoolYearStore) );

        }

        private void LogOut()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                                            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new LoginWindowView();
                Application.Current.MainWindow = loginWindow;
                _mainWindow.Close();
                //this.Hide();
                Session.Clear();
                loginWindow.Show();
            }

            return;
        }

    }
}
