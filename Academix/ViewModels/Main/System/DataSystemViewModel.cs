using Academix.Models;
using Academix.Services;
using Academix.Stores;
using Academix.ViewModels.Main.Student;
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
        private TabItemViewModel _selectedTabItem;
        public TabItemViewModel SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                if (value.Header == "Thông số hệ thống")
                {
                    value.ViewModel = new SystemParametersViewModel();
                }

                if (value.Header == "Danh sách môn học")
                {
                    value.ViewModel = new SubjectSystemViewModel();
                }
                else if (value.Header == "Danh sách loại điểm")
                {
                    value.ViewModel =  new SystemScoreTypeViewModel();
                }
                else if (value.Header == "Danh sách năm học")
                {
                    value.ViewModel =  new SystemSchoolYearViewModel();
                }
                _selectedTabItem = value;

            }
        }

        public DataSystemViewModel( NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _tabItems = new ObservableCollection<TabItemViewModel>()
        {
            new TabItemViewModel("Thông số hệ thống", new SystemParametersViewModel()),
            new TabItemViewModel("Danh sách môn học", null),
            new TabItemViewModel("Danh sách loại điểm", null),
            new TabItemViewModel("Danh sách năm học", null),
        };
            SelectedTabItem = _tabItems[0];

        }

        private ObservableCollection<TabItemViewModel> _tabItems;
        public IEnumerable<TabItemViewModel> TabItems => _tabItems;
       
        
       
        public override string ToString()
        {
            return "Hệ thống";
        }

        
    }
}
