using Academix.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
            LoadFakeRoles();
            LoadFakeAccounts();

            AddAccountCommand = new RelayCommand(AddAccount);
            DeleteAccountCommand = new RelayCommand(DeleteSelectedAccount);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void LoadFakeRoles()
        {
            RoleList.Add(new NhomNguoiDung { MaNhom = "admin", TenNhom = "Admin" });
            RoleList.Add(new NhomNguoiDung { MaNhom = "bgh", TenNhom = "Ban Giám hiệu" });
            RoleList.Add(new NhomNguoiDung { MaNhom = "giaovu", TenNhom = "Giáo vụ" });
            RoleList.Add(new NhomNguoiDung { MaNhom = "giaovien", TenNhom = "Giáo viên" });
        }

        private void LoadFakeAccounts()
        {
            AccountList.Add(new NguoiDung { TenDangNhap = "admin01", MatKhau = "1234", MaNhom = "admin" });
            AccountList.Add(new NguoiDung { TenDangNhap = "hieutruong", MatKhau = "abcd", MaNhom = "bgh" });
            AccountList.Add(new NguoiDung { TenDangNhap = "gvv01", MatKhau = "pass", MaNhom = "giaovu" });
            AccountList.Add(new NguoiDung { TenDangNhap = "gv01", MatKhau = "123", MaNhom = "giaovien" });
        }

        private void AddAccount()
        {
            if (!string.IsNullOrWhiteSpace(NewUsername) &&
                !string.IsNullOrWhiteSpace(NewPassword) &&
                !string.IsNullOrWhiteSpace(NewSelectedRole))
            {
                bool isDuplicate = AccountList.Any(acc =>
                    acc.TenDangNhap.Trim().ToLower() == NewUsername.Trim().ToLower());

                if (isDuplicate)
                {
                    System.Windows.MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    return;
                }

                var newUser = new NguoiDung
                {
                    TenDangNhap = NewUsername,
                    MatKhau = NewPassword,
                    MaNhom = NewSelectedRole
                };
                AccountList.Add(newUser);

                NewUsername = string.Empty;
                NewPassword = string.Empty;
                NewSelectedRole = null;
            }
        }


        private void DeleteSelectedAccount()
        {
            if (SelectedAccount != null)
                AccountList.Remove(SelectedAccount);
        }

        private void SaveChanges()
        {

        }
    }
}
