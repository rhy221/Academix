using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Academix.Helpers;
using Academix.Models;
using Academix.Views;
using MahApps.Metro.Controls;

namespace Academix;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow: Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        
    }

    private void LaunchGitHubSite(object sender, RoutedEventArgs e)
    {
        // Launch the GitHub site...
    }

    private void DeployCupCakes(object sender, RoutedEventArgs e)
    {
        // deploy some CupCakes...
    }

    private void HamburgerMenuControl_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
    {
        //HamburgerMenuIconItem item = (HamburgerMenuIconItem)e.InvokedItem;

        if (e.InvokedItem is HamburgerMenuIconItem item)
        {
            string label = item.Label;

            if (label == "Tài khoản")
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    this.Hide();
                    Session.Clear();

                    var loginWindow = new LoginWindowView();
                    loginWindow.Show();
                }

                return;
            }

            using var db = new PhanQuyenNguoiDungContext();
            var chucNang = db.ChucNang.FirstOrDefault(c => c.TenCN == label);
            if (chucNang == null)
            {
                MessageBox.Show("Không tìm thấy chức năng trong hệ thống.");
                return;
            }

            string viewName = chucNang.TenManHinhDuocLoad;

            if (!ViewAccessHelper.HasAccess(viewName))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var control = ViewAccessHelper.CreateViewInstance(viewName);
            if (control != null)
            {
                ContentSite.Content = control;
            }
            else
            {
                MessageBox.Show("Không tìm thấy UserControl tương ứng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    }