using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public partial class PermissionManageViewModel : ObservableObject
    {

        [ObservableProperty]
        private string inputTenNhom;

        [ObservableProperty]
        private NhomNguoiDung selectedGroup;

        public ObservableCollection<NhomNguoiDung> GroupList { get; set; } = new();

        public ICommand AddGroupCommand { get; }
        public ICommand EditGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }



        [ObservableProperty]
        private string inputTenChucNang;

        [ObservableProperty]
        private string inputManHinh;

        [ObservableProperty]
        private ChucNang selectedFunction;

        public ObservableCollection<ChucNang> FunctionList { get; set; } = new();
        public ObservableCollection<string> AvailableScreens { get; set; } = new();

        public ICommand AddFunctionCommand { get; }
        public ICommand EditFunctionCommand { get; }
        public ICommand DeleteFunctionCommand { get; }

        public PermissionManageViewModel()
        {
            AddGroupCommand = new RelayCommand(AddGroup);
            EditGroupCommand = new RelayCommand(EditGroup);
            DeleteGroupCommand = new RelayCommand(DeleteGroup);

            AddFunctionCommand = new RelayCommand(AddFunction);
            EditFunctionCommand = new RelayCommand(EditFunction);
            DeleteFunctionCommand = new RelayCommand(DeleteFunction);

            LoadFakeData();
        }

        private void LoadFakeData()
        {
            AvailableScreens.Add("SubjectReportView");
            AvailableScreens.Add("AccountManageView");
            AvailableScreens.Add("PermissionManageView");

            GroupList.Add(new NhomNguoiDung { MaNhom = "admin", TenNhom = "Admin" });
            GroupList.Add(new NhomNguoiDung { MaNhom = "bgh", TenNhom = "Ban Giám hiệu" });
            GroupList.Add(new NhomNguoiDung { MaNhom = "giaovu", TenNhom = "Giáo vụ" });
            GroupList.Add(new NhomNguoiDung { MaNhom = "giaovien", TenNhom = "Giáo viên" });

            FunctionList.Add(new ChucNang { MaCN = "CN01", TenCN = "Quản lý môn học", TenManHinhDuocLoad = "SubjectReportView" });
            FunctionList.Add(new ChucNang { MaCN = "CN02", TenCN = "Quản lý tài khoản", TenManHinhDuocLoad = "AccountManageView" });
        }

        private void AddGroup()
        {
            if (!string.IsNullOrWhiteSpace(inputTenNhom))
            {
                string maNhom = "nhom" + (GroupList.Count + 1).ToString("D2");
                GroupList.Add(new NhomNguoiDung { MaNhom = maNhom, TenNhom = inputTenNhom });
                inputTenNhom = string.Empty;
            }
        }

        private void EditGroup()
        {
            if (selectedGroup != null && !string.IsNullOrWhiteSpace(inputTenNhom))
            {
                selectedGroup.TenNhom = inputTenNhom;
            }
        }

        private void DeleteGroup()
        {
            if (selectedGroup != null)
            {
                GroupList.Remove(selectedGroup);
                selectedGroup = null;
            }
        }

        private void AddFunction()
        {
            if (!string.IsNullOrWhiteSpace(inputTenChucNang) && !string.IsNullOrWhiteSpace(inputManHinh))
            {
                string maCN = "CN" + (FunctionList.Count + 1).ToString("D2");
                FunctionList.Add(new ChucNang
                {
                    MaCN = maCN,
                    TenCN = inputTenChucNang,
                    TenManHinhDuocLoad = inputManHinh
                });
                inputTenChucNang = string.Empty;
                inputManHinh = null;
            }
        }

        private void EditFunction()
        {
            if (selectedFunction != null && !string.IsNullOrWhiteSpace(inputTenChucNang) && !string.IsNullOrWhiteSpace(inputManHinh))
            {
                selectedFunction.TenCN = inputTenChucNang;
                selectedFunction.TenManHinhDuocLoad = inputManHinh;
            }
        }

        private void DeleteFunction()
        {
            if (selectedFunction != null)
            {
                FunctionList.Remove(selectedFunction);
                selectedFunction = null;
            }
        }
    }
}
