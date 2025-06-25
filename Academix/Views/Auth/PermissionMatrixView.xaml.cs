using Academix.ViewModels.Auth;
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
    /// Interaction logic for PermissionMatrixView.xaml
    /// </summary>
    public partial class PermissionMatrixView : UserControl
    {
        public PermissionMatrixView()
        {
            InitializeComponent();
            this.DataContext = new PermissionMatrixViewModel();
            this.Loaded += PermissionMatrixView_Loaded;
        }

        private void PermissionMatrixView_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as PermissionMatrixViewModel;
            if (vm == null) return;


            while (PermissionMatrixGrid.Columns.Count > 1) 
                PermissionMatrixGrid.Columns.RemoveAt(1);


            foreach (var groupName in vm.GroupHeaders)
            {
                var column = new DataGridCheckBoxColumn
                {
                    Header = groupName,
                    Binding = new Binding($"Permissions[{groupName}]") { Mode = BindingMode.TwoWay },
                    Width = 120
                };
                PermissionMatrixGrid.Columns.Add(column);
            }
        }
    }
}
