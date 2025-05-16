using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Academix.ViewModels
{
    public class ViewClassViewModel: BaseViewModel
    {
        private ContentControl _container;
        private ContentControl _mainView;
        public ICommand BackCommand { get; }
        public ViewClassViewModel()
        {
            BackCommand = new RelayCommand(Back);
        }

        public ViewClassViewModel(ContentControl mainView,ContentControl container)
        {
            _container = container;
            _mainView = mainView;
            BackCommand = new RelayCommand(Back);
        }

       private void Back()
        {
            if (_container == null || _mainView == null)
                return;
            _container.Visibility = Visibility.Collapsed;
            _container.Content = null;
            _mainView.Visibility = Visibility.Visible;

        }

    }
}
