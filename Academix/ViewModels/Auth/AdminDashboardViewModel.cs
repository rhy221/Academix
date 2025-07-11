using Academix.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Academix.ViewModels.Auth
{
    public class AdminDashboardViewModel : ObservableObject
    {

        public bool IsAdmin => Session.MaNhom.ToUpper() == "ADMIN";

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public IRelayCommand ShowAccountManageViewCommand { get; }
        public IRelayCommand ShowPermissionManageViewCommand { get; }
        public IRelayCommand ShowPermissionMatrixViewCommand { get; }
        public IRelayCommand LogoutCommand { get; }

        public AdminDashboardViewModel()
        {
            ShowAccountManageViewCommand = new RelayCommand(() =>
                CurrentView = new Views.AccountManageView());

            ShowPermissionManageViewCommand = new RelayCommand(() =>
                CurrentView = new Views.PermissionManageView());

            ShowPermissionMatrixViewCommand = new RelayCommand(() =>
                CurrentView = new Views.PermissionMatrixView());

            LogoutCommand = new RelayCommand(ExecuteLogout);

            CurrentView = new Views.AccountManageView();
        }

        private void ExecuteLogout()
        {
            var login = new Views.LoginWindowView();
            login.Show();

            Application.Current.Windows
                .OfType<Window>()
                .Where(w => !(w is Views.LoginWindowView))
                .ToList()
                .ForEach(w => w.Close());
        }




    }


}
