using Academix.Models;
using Academix.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        private NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        private BaseViewModel _sideBarViewModel;
        public BaseViewModel SideBarViewModel => _sideBarViewModel;
        public String CurrentViewModelName => _navigationStore.CurrentViewModel.ToString();
        private ObservableCollection<SchoolYear> _schoolYears;
        public IEnumerable<SchoolYear> SchoolYears => _schoolYears;
        public SchoolYear SelectedShoolYear { get; set; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _sideBarViewModel = new HamburgerMenuViewModel(new Services.NavigationService(navigationStore));
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _schoolYears = new ObservableCollection<SchoolYear>();
            _schoolYears.Add(new SchoolYear("111", 2022, 2023));
            _schoolYears.Add(new SchoolYear("111", 2023, 2024));
            _schoolYears.Add(new SchoolYear("111", 2024, 2025));

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(CurrentViewModelName));
        }
    }
}
