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
using Academix.ViewModels.Auth;


namespace Academix.Views
{
    /// <summary>
    /// Interaction logic for AccountManageView.xaml
    /// </summary>
    public partial class AccountManageView : UserControl
    {
        public AccountManageView()
        {
            InitializeComponent();
            this.DataContext = new AccountManageViewModel();
        }
    }
}
