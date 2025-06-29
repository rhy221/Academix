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
        private BaseViewModel _viewModel;
        public BaseViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        public TabItemViewModel(string header, BaseViewModel viewModel)
        {
            Header = header;
            _viewModel = viewModel;
        }
    }
}
