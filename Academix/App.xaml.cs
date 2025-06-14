using Academix.Stores;
using Academix.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Academix;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private NavigationStore _navigationStore;

    public App()
    {
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = new StudentViewModel();

        MainWindow = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };

        MainWindow.Show();

        base.OnStartup(e);
    }

}

