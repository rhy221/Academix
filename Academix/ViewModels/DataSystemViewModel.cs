using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    class DataSystemViewModel:BaseViewModel
    {

        private ObservableCollection<TabItemViewModel> _tabItems = new ObservableCollection<TabItemViewModel>()
        {
            new TabItemViewModel("Thông số hệ thống", new SystemParametersViewModel()),
            new TabItemViewModel("Danh sách môn học", new SubjectSystemViewModel()),
            new TabItemViewModel("Danh sách loại điểm", new SystemScoreTypeViewModel()),
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
        public DataSystemViewModel()
        {

        }
        public override string ToString()
        {
            SelectedTabItem = _tabItems[0];
            return "Hệ thống";
        }

        
    }
}
