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
    public partial class GradeView : UserControl
    {
        public GradeView()
        {
            InitializeComponent();
            this.DataContext = new GradeViewModel();
        }

        // THÊM MỚI: Hàm xử lý lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Tìm DataGrid trong view (ví dụ bạn đặt tên trong XAML là MyDataGrid)

            var dataGrid = this.FindName("MyDataGrid") as DataGrid;
            if (dataGrid != null)
            {
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(dataGrid), dataGrid);
                dataGrid.CommitEdit(DataGridEditingUnit.Cell, true);
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            }

            // Gọi SaveChanges trong ViewModel
            if (this.DataContext is GradeViewModel vm)
            {
                vm.SaveChangesCommand.Execute(null);

            }
        }
    }
}
