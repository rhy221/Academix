using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Academix.Views
{
    /// <summary>
    /// Interaction logic for SearchStudentView.xaml
    /// </summary>
    public partial class SearchStudentView : UserControl
    {
        public SearchStudentView()
        {
            InitializeComponent();
        }

        private void ModifyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Collapsed;
            ExtendView.Visibility = Visibility.Visible;
            ExtendView.Content = new ModifyStudentViewModel(MainView, ExtendView);
        }

        private void ViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Collapsed;
            ExtendView.Visibility = Visibility.Visible;
            ExtendView.Content = new ViewStudentViewModel(MainView, ExtendView);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Collapsed;
            ExtendView.Visibility = Visibility.Visible;
            ExtendView.Content = new AddStudentViewModel(MainView, ExtendView);
        }
    }
}
