using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
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
        public ICommand SaveGroupsCommand { get; }

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
        public ICommand SaveFunctionsCommand { get; }

        public PermissionManageViewModel()
        {
            AddGroupCommand = new RelayCommand(AddGroup);
            EditGroupCommand = new RelayCommand(EditGroup);
            DeleteGroupCommand = new RelayCommand(DeleteGroup);
            SaveGroupsCommand = new RelayCommand(SaveGroupChanges);

            AddFunctionCommand = new RelayCommand(AddFunction);
            EditFunctionCommand = new RelayCommand(EditFunction);
            DeleteFunctionCommand = new RelayCommand(DeleteFunction);
            SaveFunctionsCommand = new RelayCommand(SaveFunctionChanges);

            LoadDataFromDb();
        }

        private void LoadDataFromDb()
        {
            using var db = new PhanQuyenNguoiDungContext();

            GroupList = new ObservableCollection<NhomNguoiDung>(db.NhomNguoiDung.AsNoTracking().ToList());
            FunctionList = new ObservableCollection<ChucNang>(db.ChucNang.AsNoTracking().ToList());
            AvailableScreens = new ObservableCollection<string>(
                FunctionList.Select(f => f.TenManHinhDuocLoad).Distinct().OrderBy(s => s));

            OnPropertyChanged(nameof(GroupList));
            OnPropertyChanged(nameof(FunctionList));
            OnPropertyChanged(nameof(AvailableScreens));
        }

        private void AddGroup()
        {
            if (!string.IsNullOrWhiteSpace(InputTenNhom))
            {
                using var db = new PhanQuyenNguoiDungContext();
                string maNhom = GenerateMaNhomFromTenNhom(InputTenNhom);
                int suffix = 1;
                string original = maNhom;
                while (db.NhomNguoiDung.Any(n => n.MaNhom == maNhom))
                    maNhom = original + suffix++.ToString("D2");

                var newGroup = new NhomNguoiDung { MaNhom = maNhom, TenNhom = InputTenNhom };
                db.NhomNguoiDung.Add(newGroup);
                db.SaveChanges();
                GroupList.Add(newGroup);
                InputTenNhom = string.Empty;
            }
        }

        private void EditGroup()
        {
            if (SelectedGroup != null && !string.IsNullOrWhiteSpace(InputTenNhom))
            {
                using var db = new PhanQuyenNguoiDungContext();
                var entity = db.NhomNguoiDung.Find(SelectedGroup.MaNhom);
                if (entity != null)
                {
                    entity.TenNhom = InputTenNhom;
                    db.SaveChanges();
                    SelectedGroup.TenNhom = InputTenNhom;
                }
            }
        }

        private void DeleteGroup()
        {
            if (SelectedGroup != null)
            {
                using var db = new PhanQuyenNguoiDungContext();
                var entity = db.NhomNguoiDung.Find(SelectedGroup.MaNhom);
                if (entity != null)
                {
                    db.NhomNguoiDung.Remove(entity);
                    db.SaveChanges();
                    GroupList.Remove(SelectedGroup);
                    SelectedGroup = null;
                }
            }
        }

        private void SaveGroupChanges()
        {
            using var db = new PhanQuyenNguoiDungContext();
            foreach (var group in GroupList)
            {
                var existing = db.NhomNguoiDung.FirstOrDefault(g => g.MaNhom == group.MaNhom);
                if (existing != null && existing.TenNhom != group.TenNhom)
                {
                    existing.TenNhom = group.TenNhom;
                }
            }
            db.SaveChanges();
            MessageBox.Show("Đã lưu thay đổi nhóm người dùng vào CSDL!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddFunction()
        {
            if (!string.IsNullOrWhiteSpace(InputTenChucNang) && !string.IsNullOrWhiteSpace(InputManHinh))
            {
                using var db = new PhanQuyenNguoiDungContext();
                string maCN = GenerateMaCNFromTenChucNang(InputTenChucNang);
                int suffix = 1;
                string original = maCN;
                while (db.ChucNang.Any(c => c.MaCN == maCN))
                    maCN = original + suffix++.ToString("D2");

                var newFunc = new ChucNang { MaCN = maCN, TenCN = InputTenChucNang, TenManHinhDuocLoad = InputManHinh };
                db.ChucNang.Add(newFunc);
                db.SaveChanges();
                FunctionList.Add(newFunc);
                if (!AvailableScreens.Contains(InputManHinh))
                    AvailableScreens.Add(InputManHinh);
                InputTenChucNang = string.Empty;
                InputManHinh = null;
            }
        }

        private void EditFunction()
        {
            if (SelectedFunction != null && !string.IsNullOrWhiteSpace(InputTenChucNang) && !string.IsNullOrWhiteSpace(InputManHinh))
            {
                using var db = new PhanQuyenNguoiDungContext();
                var entity = db.ChucNang.Find(SelectedFunction.MaCN);
                if (entity != null)
                {
                    entity.TenCN = InputTenChucNang;
                    entity.TenManHinhDuocLoad = InputManHinh;
                    db.SaveChanges();
                    SelectedFunction.TenCN = InputTenChucNang;
                    SelectedFunction.TenManHinhDuocLoad = InputManHinh;
                }
            }
        }

        private void DeleteFunction()
        {
            if (SelectedFunction != null)
            {
                using var db = new PhanQuyenNguoiDungContext();
                var entity = db.ChucNang.Find(SelectedFunction.MaCN);
                if (entity != null)
                {
                    db.ChucNang.Remove(entity);
                    db.SaveChanges();
                    FunctionList.Remove(SelectedFunction);
                    SelectedFunction = null;
                }
            }
        }

        private void SaveFunctionChanges()
        {
            using var db = new PhanQuyenNguoiDungContext();
            foreach (var func in FunctionList)
            {
                var existing = db.ChucNang.FirstOrDefault(c => c.MaCN == func.MaCN);
                if (existing != null && (existing.TenCN != func.TenCN || existing.TenManHinhDuocLoad != func.TenManHinhDuocLoad))
                {
                    existing.TenCN = func.TenCN;
                    existing.TenManHinhDuocLoad = func.TenManHinhDuocLoad;
                }
            }
            db.SaveChanges();
            MessageBox.Show("Đã lưu thay đổi chức năng vào CSDL!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string GenerateMaNhomFromTenNhom(string tenNhom)
        {
            string normalized = RemoveDiacritics(tenNhom).ToUpper();
            if (normalized.Length <= 10)
                return new string(normalized.Where(c => !char.IsWhiteSpace(c)).ToArray());
            return string.Concat(normalized.Split(' ').Select(word => word[0]));
        }

        private string GenerateMaCNFromTenChucNang(string tenCN)
        {
            string normalized = RemoveDiacritics(tenCN).ToUpper();
            var initials = string.Concat(normalized.Split(' ').Select(w => w[0]));
            return initials.Length < 3 ? "CN" + initials : initials;
        }

        private string RemoveDiacritics(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormD);
            return new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
        }
    }
}
