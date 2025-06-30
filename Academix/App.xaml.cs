using Academix.Models;
using Academix.Stores;
using Academix.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Academix;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{


    protected override async void OnStartup(StartupEventArgs e)
    {
        FrameworkElement.LanguageProperty.OverrideMetadata(
    typeof(FrameworkElement),
    new FrameworkPropertyMetadata(
        XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
        var loginWindow = new Academix.Views.LoginWindowView();

        Application.Current.MainWindow = loginWindow;

        loginWindow.Show();

        base.OnStartup(e);
    }

}

