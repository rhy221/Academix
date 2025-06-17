using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class HamburgerMenuViewModel:BaseViewModel
    {

        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
        public ICommand StudentNavigateCommand { get; }
        public ICommand ClassNavigateCommand { get; }
        public ICommand GradeNavigateCommand { get; }
        public ICommand ReportNavigateCommand { get; }
        public ICommand DataSystemNavigateCommand { get; }

        public HamburgerMenuViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            StudentNavigateCommand = new RelayCommand(StudentNavigate);
            ClassNavigateCommand = new RelayCommand(ClassNaviget);
            GradeNavigateCommand = new RelayCommand(GradeNavigate);
            ReportNavigateCommand = new RelayCommand(ReportNavigate);
            DataSystemNavigateCommand = new RelayCommand(DataSystemNavigate);

            _navigationService.Navigate(new StudentsViewModel(_navigationService, _schoolYearStore));
        }

        private void StudentNavigate()
        {
            _navigationService.Navigate(new StudentsViewModel(_navigationService, _schoolYearStore));
        }

        private void ClassNaviget()
        {
            _navigationService.Navigate(new ClassesViewModel(_navigationService, _schoolYearStore));

        }

        private void GradeNavigate()
        {
            _navigationService.Navigate(new GradeViewModel(_navigationService, _schoolYearStore) );

        }

        private void ReportNavigate()
        {
            _navigationService.Navigate(new ReportViewModel(_navigationService, _schoolYearStore) );

        }

        private void DataSystemNavigate()
        {
            _navigationService.Navigate(new DataSystemViewModel(_navigationService, _schoolYearStore) );

        }

    }
}
