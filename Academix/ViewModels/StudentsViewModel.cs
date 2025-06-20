using Academix.Models;
using Academix.Services;
using Academix.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Academix.ViewModels
{
    public class StudentsViewModel: BaseViewModel
    {
        private SchoolYearStore _schoolYearStore;
        private NavigationService _navigationService;
        private ObservableCollection<TabItemViewModel> _tabItems;
        public ObservableCollection<TabItemViewModel> TabItems => _tabItems;

        public StudentsViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _tabItems = new ObservableCollection<TabItemViewModel>()
            {
                new TabItemViewModel("Tìm kiếm học sinh", new SearchStudentViewModel(_navigationService, _schoolYearStore, this)),
                new TabItemViewModel("Tiếp nhận học sinh đầu cấp", new AddFreshmanViewModel(_navigationService, _schoolYearStore)),
                new TabItemViewModel("Phân lớp", new ClassPlacementViewModel(_navigationService, _schoolYearStore)),
            };
        }

       

        public override string ToString()
        {
            return "Học sinh";
        }
    }
}
