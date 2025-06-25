using Academix.Models;
using Academix.Services;
using Academix.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.Report
{
    public class ReportViewModel:BaseViewModel
    {

        private ObservableCollection<TabItemViewModel> _tabItems;
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

        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;

        public ReportViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _tabItems = new ObservableCollection<TabItemViewModel>()
            {
            new TabItemViewModel("Báo cáo tổng kết môn học", new SubjectReportViewModel(_schoolYearStore)),
            new TabItemViewModel("Báo cáo tổng kết cuổi kỳ", new SemesterReportViewModel(_schoolYearStore)),
            };
            SelectedTabItem = _tabItems[0];
        }

       
        public override string ToString()
        {
            return "Báo cáo";
        }
    }
}
