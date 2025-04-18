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
        HamburgerMenuIconItem item = (HamburgerMenuIconItem)e.InvokedItem;

        switch (item.Label)
        {
            case "Hệ thống":
                ContentSite.Content = new DataSystemView();
                break;
            case "Học sinh":
                ContentSite.Content = new StudentView();
                break;
            case "Lớp":
                ContentSite.Content = new ClassesView();
                break;
            case "Điểm":
                ContentSite.Content = new GradeView();
                break;
            case "Báo cáo":
                ContentSite.Content = new ReportView();
                break;
        }
    }
    }