using Academix.Models;
using Academix.Services;
using Academix.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Academix.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        private NavigationStore _navigationStore;
        private SchoolYearStore _schoolYearStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        private BaseViewModel _sideBarViewModel;
        public BaseViewModel SideBarViewModel => _sideBarViewModel;
        public String CurrentViewModelName => _navigationStore.CurrentViewModel.ToString();
        private ObservableCollection<Namhoc> _schoolYears;
        public ObservableCollection<Namhoc> SchoolYears
        {
            get
            {
                return _schoolYears;
            }
            set
            {
                _schoolYears = value;
                OnPropertyChanged(nameof(SchoolYears));

            }
        }
        public Namhoc SelectedSchoolYear
        {
            get
            {
                return _schoolYearStore.SelectedSchoolYear;
            }
            set
            {
                _schoolYearStore.SelectedSchoolYear = value;
                OnPropertyChanged(nameof(SelectedSchoolYear));
            }
        }
        
        public MainViewModel(NavigationStore navigationStore, SchoolYearStore schoolYearStore )
        {
            _navigationStore = navigationStore;
            _schoolYearStore = schoolYearStore;
            _sideBarViewModel = new HamburgerMenuViewModel(new NavigationService(navigationStore), _schoolYearStore);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _schoolYears = new ObservableCollection<Namhoc>(_schoolYearStore.SchoolYears);
            
           
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(CurrentViewModelName));
        }

        
    }
}
