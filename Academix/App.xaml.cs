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
    private NavigationStore _navigationStore;
    private SchoolYearStore _schoolYearStore;

    public App()
    {
        _navigationStore = new NavigationStore();
        _schoolYearStore = new SchoolYearStore();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        List<Namhoc> schoolYears;
        
        using (var context = new QuanlyhocsinhContext())
        {
            schoolYears = await context.Namhocs.ToListAsync();
        }

        Namhoc allSchoolYear = new Namhoc();
        allSchoolYear.IsAll = true;
        schoolYears.Insert(0, allSchoolYear);
        _schoolYearStore.SchoolYears = schoolYears;
        _schoolYearStore.SelectedSchoolYear = allSchoolYear;


        MainWindow = new MainWindow() { DataContext = new MainViewModel(_navigationStore, _schoolYearStore) };

        MainWindow.Show();

        base.OnStartup(e);
    }

}

