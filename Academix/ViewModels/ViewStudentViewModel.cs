using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Academix.Models;

namespace Academix.ViewModels
{
    public class ViewStudentViewModel
    {
        public StudentDisplayModel Student { get; }
        private ContentControl _mainView;
        private ContentControl _container;
        public ICommand BackCommand { get; }
        public ViewStudentViewModel()
        {
            BackCommand = new RelayCommand(Back);
        }
        

        public ViewStudentViewModel(ContentControl mainView, ContentControl container)
        {
            _container = container;
            _mainView = mainView;
            BackCommand = new RelayCommand(Back);
        //    Student = SelectedStudent;
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

