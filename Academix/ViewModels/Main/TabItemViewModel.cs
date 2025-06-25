using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Academix.ViewModels.Main
{
    public class TabItemViewModel:BaseViewModel
    {
        public string Header { get; set; }
        public BaseViewModel ViewModel { get; set; }  

        public TabItemViewModel(string header, BaseViewModel viewModel)
        {
            Header = header;
            ViewModel = viewModel;
        }
    }
}
