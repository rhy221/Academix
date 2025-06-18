using System.Configuration;
using System.Data;
using System.Windows;

namespace Academix;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var loginWindow = new Academix.Views.LoginWindowView();

        Application.Current.MainWindow = loginWindow;

        loginWindow.Show();
    }

}

