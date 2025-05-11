using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Academix.Models;

namespace Academix.ViewModels
{
    public class SubjectReportViewModel : INotifyPropertyChanged
    {
        private string selectedSubject;
        private string selectedYear;
        private string selectedSemester;
        private ObservableCollection<ClassroomReportItem> filteredReports;

        public ObservableCollection<SubjectReport> AllReports { get; set; }

        public string SelectedSubject
        {
            get => selectedSubject;
            set
            {
                selectedSubject = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                selectedYear = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public string SelectedSemester
        {
            get => selectedSemester;
            set
            {
                selectedSemester = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public ObservableCollection<ClassroomReportItem> FilteredReports
        {
            get => filteredReports;
            set
            {
                filteredReports = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExportCommand { get; }

        public SubjectReportViewModel()
        {
            AllReports = new ObservableCollection<SubjectReport>();
            FilteredReports = new ObservableCollection<ClassroomReportItem>();
            ExportCommand = new RelayCommand(ExecuteExport);

            LoadData();
            FilterData();
        }

        private void LoadData()
        {

        }

        private void FilterData()
        {
            var matchedReports = AllReports
                .Where(r =>
                    (string.IsNullOrEmpty(SelectedSubject) || r.Subject.Name.Contains(SelectedSubject, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(SelectedYear) || r.SchoolYear.Contains(SelectedYear, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(SelectedSemester) || r.Semester.Contains(SelectedSemester, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();

            var result = new ObservableCollection<ClassroomReportItem>();

            foreach (var report in matchedReports)
            {
                foreach (var row in report.Table)
                {
                    result.Add(new ClassroomReportItem
                    {
                        SubjectName = report.Subject.Name,
                        Semester = report.Semester,
                        SchoolYear = report.SchoolYear,
                        ClassroomID = row.ClassroomID,
                        ClassroomSize = row.ClassroomSize,
                        PassedNum = row.PassedNum,
                        Percentage = row.Percentage
                    });
                }
            }

            FilteredReports = result;
        }

        private void ExecuteExport(object obj)
        {
            // TODO: Viết logic xuất file hoặc xử lý dữ liệu
            // Ví dụ:
            Console.WriteLine("Exporting subject report...");
            foreach (var item in FilteredReports)
            {
                Console.WriteLine($"{item.SubjectName} - {item.ClassroomID} - {item.Percentage}%");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class ClassroomReportItem
    {
        public string SubjectName { get; set; }
        public string Semester { get; set; }
        public string SchoolYear { get; set; }
        public string ClassroomID { get; set; }
        public int ClassroomSize { get; set; }
        public int PassedNum { get; set; }
        public float Percentage { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
