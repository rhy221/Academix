using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input; 
using Academix.Models;
using CommunityToolkit.Mvvm.Input;

namespace Academix.ViewModels
{
    public class SemesterReportViewModel : INotifyPropertyChanged
    {
        private string selectedYear;
        private string selectedSemester;
        private ObservableCollection<ClassDisplayData> filteredClasses;

        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                selectedYear = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSemester
        {
            get => selectedSemester;
            set
            {
                selectedSemester = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SemesterReport> AllReports { get; set; }

        public ObservableCollection<string> AllYears { get; set; }
        public ObservableCollection<string> AllSemesters { get; set; }

        public ObservableCollection<ClassDisplayData> FilteredClasses
        {
            get => filteredClasses;
            set
            {
                filteredClasses = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExportDataCommand { get; private set; }

        public SemesterReportViewModel()
        {
            AllReports = new ObservableCollection<SemesterReport>();
            LoadDataFromDatabase(); 

            AllYears = new ObservableCollection<string>
            {
                
            };

            AllSemesters = new ObservableCollection<string>
            {
                
            };

            ExportDataCommand = new RelayCommand(ExportData);  
        }

        private void LoadDataFromDatabase()
        {
            
        }

        public void ExportData()
        {
            if (!string.IsNullOrEmpty(SelectedYear) && !string.IsNullOrEmpty(SelectedSemester))
            {
                FilterData(); 
            }
            else
            {
                
            }
        }

        private void FilterData()
        {
            var filtered = AllReports
                .Where(r => r.SchoolYear == SelectedYear && r.Semester == SelectedSemester)
                .SelectMany(r => r.Table.Select(row => new ClassDisplayData
                {
                    ClassName = row.ClassroomID,
                    TotalStudents = row.ClassroomSize,
                    PassedStudents = row.PassedNum,
                    PassRate = row.ClassroomSize > 0 ? (double)row.PassedNum / row.ClassroomSize * 100 : 0
                }));

           
            FilteredClasses = new ObservableCollection<ClassDisplayData>(filtered);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    
    public class ClassDisplayData
    {
        public string ClassName { get; set; }
        public int TotalStudents { get; set; }
        public int PassedStudents { get; set; }
        public double PassRate { get; set; }
    }
}
