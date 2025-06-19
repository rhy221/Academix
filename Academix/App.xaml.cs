using Academix.Models;
using Academix.Stores;
using Academix.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Academix;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  

    protected override async void OnStartup(StartupEventArgs e)
    {
        var loginWindow = new Academix.Views.LoginWindowView();

        Application.Current.MainWindow = loginWindow;

        loginWindow.Show();

        base.OnStartup(e);
    }

}

