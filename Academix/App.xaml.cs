using Academix.Models;
using Academix.Stores;
using Academix.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.IO;
using System.Text.RegularExpressions;
namespace Academix;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{


    protected override async void OnStartup(StartupEventArgs e)
    {
        //string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;TrustServerCertificate=True";
        //string PQNDscript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DbScripts\PQNDscript.sql"));
        //string[] PQNDscriptcommands = PQNDscript.Split(new[] { "GO", "go", "Go", "gO" }, StringSplitOptions.RemoveEmptyEntries);
        //using (var connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();
        //    foreach (var cmd in PQNDscriptcommands)
        //    {
        //        using (var command = new SqlCommand(cmd, connection))
        //        {
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //string QLHSscript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DbScripts\QLHSscript.sql"));
        //string[] QLHSscriptcommands = QLHSscript.Split(new[] { "GO", "go", "Go", "gO" }, StringSplitOptions.RemoveEmptyEntries);

        //using (var connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();
        //    foreach (var cmd in QLHSscriptcommands)
        //    {
        //        using (var command = new SqlCommand(cmd, connection))
        //        {
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

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

