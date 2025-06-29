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

namespace Academix.ViewModels.Main.Student
{
    public class StudentsViewModel: BaseViewModel
    {
        private SchoolYearStore _schoolYearStore;
        private NavigationService _navigationService;
        private ObservableCollection<TabItemViewModel> _tabItems;
        public ObservableCollection<TabItemViewModel> TabItems => _tabItems;
        private TabItemViewModel _selectedTabItem;
        public TabItemViewModel SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                if(value.Header == "Tìm kiếm học sinh")
                {
                    value.ViewModel = new SearchStudentViewModel(_navigationService, _schoolYearStore, this);
                }

                if (value.Header == "Tiếp nhận học sinh")
                {
                    value.ViewModel = new AddFreshmanViewModel(_navigationService, _schoolYearStore);
                }
                else if(value.Header == "Phân lớp")
                {
                    value.ViewModel = new ClassPlacementViewModel(_navigationService, _schoolYearStore);
                }
                _selectedTabItem = value;
                
            }
        }

        public StudentsViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _tabItems = new ObservableCollection<TabItemViewModel>()
            {
                new TabItemViewModel("Tìm kiếm học sinh", new SearchStudentViewModel(_navigationService, _schoolYearStore, this)),
                new TabItemViewModel("Tiếp nhận học sinh", null),
                new TabItemViewModel("Phân lớp", null),
            };
            SelectedTabItem = _tabItems[0];
        }

       

        public override string ToString()
        {
            return "Học sinh";
        }
    }
}
