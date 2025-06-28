using Academix.Models;
using Academix.Services;
using Academix.Stores;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.System
{
    class DataSystemViewModel:BaseViewModel
    {
        private SchoolYearStore _schoolYearStore;
        private NavigationService _navigationService;

        public DataSystemViewModel( NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
        }

        private ObservableCollection<TabItemViewModel> _tabItems = new ObservableCollection<TabItemViewModel>()
        {
            new TabItemViewModel("Thông số hệ thống", new SystemParametersViewModel()),
            new TabItemViewModel("Danh sách môn học", new SubjectSystemViewModel()),
            new TabItemViewModel("Danh sách loại điểm", new SystemScoreTypeViewModel()),
            new TabItemViewModel("Danh sách học kỳ", new SystemSemesterViewModel()),
            new TabItemViewModel("Danh sách năm học", new SystemSchoolYearViewModel()),
        };
        public IEnumerable<TabItemViewModel> TabItems => _tabItems;
        private TabItemViewModel _selectedTabItem;
        public TabItemViewModel SelectedTabItem
        {
            get
            {
             
                return _selectedTabItem;
            }
            set
            {
                _selectedTabItem = value;
            }
        }
       
        public override string ToString()
        {
            SelectedTabItem = _tabItems[0];
            return "Hệ thống";
        }

        
    }
}
