using Academix.Services;
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
        public ICommand StudentNavigateCommand { get; }
        public ICommand ClassNavigateCommand { get; }
        public ICommand GradeNavigateCommand { get; }
        public ICommand ReportNavigateCommand { get; }
        public ICommand DataSystemNavigateCommand { get; }

        public HamburgerMenuViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            StudentNavigateCommand = new RelayCommand(StudentNavigate);
            ClassNavigateCommand = new RelayCommand(ClassNaviget);
            GradeNavigateCommand = new RelayCommand(GradeNavigate);
            ReportNavigateCommand = new RelayCommand(ReportNavigate);
            DataSystemNavigateCommand = new RelayCommand(DataSystemNavigate);
        }

        private void StudentNavigate()
        {
            _navigationService.Navigate(new StudentViewModel());
        }

        private void ClassNaviget()
        {
            _navigationService.Navigate(new ClassesViewModel());

        }

        private void GradeNavigate()
        {
            _navigationService.Navigate(new GradeViewModel());

        }

        private void ReportNavigate()
        {
            _navigationService.Navigate(new ReportViewModel());

        }

        private void DataSystemNavigate()
        {
            _navigationService.Navigate(new DataSystemViewModel());

        }

    }
}
