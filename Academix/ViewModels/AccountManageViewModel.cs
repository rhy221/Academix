using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class AccountManageViewModel : ObservableObject
    {
        public ObservableCollection<NguoiDung> AccountList { get; set; } = new();
        public ObservableCollection<NhomNguoiDung> RoleList { get; set; } = new();

        private string _newUsername;
        public string NewUsername
        {
            get => _newUsername;
            set => SetProperty(ref _newUsername, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _newSelectedRole;
        public string NewSelectedRole
        {
            get => _newSelectedRole;
            set => SetProperty(ref _newSelectedRole, value);
        }

        private NguoiDung _selectedAccount;
        public NguoiDung SelectedAccount
        {
            get => _selectedAccount;
            set => SetProperty(ref _selectedAccount, value);
        }

        public ICommand AddAccountCommand { get; }
        public ICommand DeleteAccountCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public AccountManageViewModel()
        {
            LoadAccountsFromDb();
            LoadRolesFromDb();

            AddAccountCommand = new RelayCommand(AddAccount);
            DeleteAccountCommand = new RelayCommand(DeleteSelectedAccount);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void LoadAccountsFromDb()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var users = db.NguoiDung.AsNoTracking().ToList();
            AccountList = new ObservableCollection<NguoiDung>(users);
            OnPropertyChanged(nameof(AccountList));
        }

        private void LoadRolesFromDb()
        {
            using var db = new PhanQuyenNguoiDungContext();
            var roles = db.NhomNguoiDung.AsNoTracking().ToList();
            RoleList = new ObservableCollection<NhomNguoiDung>(roles);
            OnPropertyChanged(nameof(RoleList));
        }

        private void AddAccount()
        {
            if (!string.IsNullOrWhiteSpace(NewUsername) &&
                !string.IsNullOrWhiteSpace(NewPassword) &&
                !string.IsNullOrWhiteSpace(NewSelectedRole))
            {
                using var db = new PhanQuyenNguoiDungContext();
                bool exists = db.NguoiDung.Any(u => u.TenDangNhap == NewUsername);
                if (exists)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newUser = new NguoiDung
                {
                    TenDangNhap = NewUsername,
                    MatKhau = NewPassword,
                    MaNhom = NewSelectedRole
                };

                db.NguoiDung.Add(newUser);
                db.SaveChanges();

                AccountList.Add(newUser);

                NewUsername = string.Empty;
                NewPassword = string.Empty;
                NewSelectedRole = null;
            }
        }

        private void DeleteSelectedAccount()
        {
            if (SelectedAccount != null)
            {
                using var db = new PhanQuyenNguoiDungContext();
                var user = db.NguoiDung.Find(SelectedAccount.TenDangNhap);
                if (user != null)
                {
                    db.NguoiDung.Remove(user);
                    db.SaveChanges();
                }

                AccountList.Remove(SelectedAccount);
            }
        }

        private void SaveChanges()
        {
            using var db = new PhanQuyenNguoiDungContext();

            foreach (var user in AccountList)
            {
                var existing = db.NguoiDung.FirstOrDefault(u => u.TenDangNhap == user.TenDangNhap);
                if (existing != null &&
                    (existing.MatKhau != user.MatKhau || existing.MaNhom != user.MaNhom))
                {
                    existing.MatKhau = user.MatKhau;
                    existing.MaNhom = user.MaNhom;
                }
            }

            db.SaveChanges();
            MessageBox.Show("Đã lưu các thay đổi vào cơ sở dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
