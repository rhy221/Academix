using Academix.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class SubjectReportViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> SemesterList { get; set; }
        public ObservableCollection<string> AcademicYearList { get; }
        public ObservableCollection<Subject> SubjectList { get; }

        private string? _selectedSemester;
        public string? SelectedSemester
        {
            get => _selectedSemester;
            set
            {
                if (_selectedSemester != value)
                {
                    _selectedSemester = value;
                    OnPropertyChanged(nameof(SelectedSemester));
                }
            }
        }

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (_selectedSubject != value)
                {
                    _selectedSubject = value;
                    OnPropertyChanged(nameof(SelectedSubject));
                }
            }
        }

        private string? _selectedAcademicYear;
        public string? SelectedAcademicYear
        {
            get => _selectedAcademicYear;
            set
            {
                if (_selectedAcademicYear != value)
                {
                    _selectedAcademicYear = value;
                    OnPropertyChanged(nameof(SelectedAcademicYear));
                }
            }
        }

        private string _subject = "";
        public string Subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(nameof(Subject)); }
        }

        private string _semester = "";
        public string Semester
        {
            get => _semester;
            set { _semester = value; OnPropertyChanged(nameof(Semester)); }
        }

        private string _academicYear = "";
        public string AcademicYear
        {
            get => _academicYear;
            set { _academicYear = value; OnPropertyChanged(nameof(AcademicYear)); }
        }

        private ObservableCollection<ClassroomReportItem> _classReportList = new ObservableCollection<ClassroomReportItem>();
        public ObservableCollection<ClassroomReportItem> ClassReportList
        {
            get => _classReportList;
            set { _classReportList = value; OnPropertyChanged(nameof(ClassReportList)); }
        }

        public ObservableCollection<SubjectReport> AllReports { get; set; } = new ObservableCollection<SubjectReport>();

        public ICommand ExportReportCommand { get; }
        public ICommand ExportPdfCommand { get; }
        public ICommand ExportExcelCommand { get; }

        public SubjectReportViewModel()
        {
            SubjectList = new ObservableCollection<Subject>
            {
                new Subject("Math", "Toán học"),
                new Subject("Phys", "Vật lý"),
                new Subject("Chem", "Hóa học")
            };

            SemesterList = new ObservableCollection<string>
            {
                "HK1",
                "HK2",
                "HK3"
            };

            AcademicYearList = new ObservableCollection<string>
            {
                "2022-2023",
                "2023-2024",
                "2024-2025"
            };

            Subject = SelectedSubject?.Name ?? "";
            Semester = SelectedSemester ?? "";
            AcademicYear = SelectedAcademicYear ?? "";

            ExportReportCommand = new RelayCommand(ExportReport);
            ExportPdfCommand = new RelayCommand(ExportPdf);
            ExportExcelCommand = new RelayCommand(ExportExcel);

            LoadFakeReports();
        }

        private void LoadFakeReports()
        {
            var math = SubjectList.First(s => s.ID == "Math");
            var phys = SubjectList.First(s => s.ID == "Phys");
            var chem = SubjectList.First(s => s.ID == "Chem");

            var mathReport1 = new SubjectReport("M001", "HK1", "2024-2025", math);
            mathReport1.setTable("10A1", 30, 25);
            mathReport1.setTable("10A2", 28, 21);
            AllReports.Add(mathReport1);

            var mathReport2 = new SubjectReport("M002", "HK2", "2024-2025", math);
            mathReport2.setTable("10A1", 29, 24);
            mathReport2.setTable("10A3", 27, 23);
            AllReports.Add(mathReport2);

            var physReport1 = new SubjectReport("P001", "HK1", "2024-2025", phys);
            physReport1.setTable("10A1", 30, 20);
            physReport1.setTable("10A2", 28, 18);
            AllReports.Add(physReport1);

            var physReport2 = new SubjectReport("P002", "HK2", "2024-2025", phys);
            physReport2.setTable("10A3", 27, 19);
            physReport2.setTable("10A4", 26, 20);
            AllReports.Add(physReport2);

            var chemReport1 = new SubjectReport("C001", "HK1", "2024-2025", chem);
            chemReport1.setTable("10A2", 28, 22);
            chemReport1.setTable("10A4", 26, 21);
            AllReports.Add(chemReport1);

            var chemReport2 = new SubjectReport("C002", "HK2", "2024-2025", chem);
            chemReport2.setTable("10A1", 30, 27);
            chemReport2.setTable("10A3", 27, 25);
            AllReports.Add(chemReport2);
        }


        private void FilterData()
        {
            ClassReportList.Clear();

            if (string.IsNullOrEmpty(SelectedAcademicYear))
            {
                SelectedAcademicYear = "2024-2025";
                OnPropertyChanged(nameof(SelectedAcademicYear));
            }

            if (SelectedSubject == null || SelectedSemester == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Môn học và Học kỳ.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var report = AllReports.FirstOrDefault(r =>
                r.Subject.ID == SelectedSubject.ID &&
                r.Semester == SelectedSemester &&
                r.SchoolYear == SelectedAcademicYear);

            if (report == null)
                return;

            int stt = 1;
            foreach (var item in report.Table)
            {
                var tiLe = item.ClassroomSize > 0
                    ? (item.PassedNum * 100f / item.ClassroomSize).ToString("0.##") + "%"
                    : "0%";

                ClassReportList.Add(new ClassroomReportItem
                {
                    STT = stt++,
                    Lop = item.ClassroomID,
                    SiSo = item.ClassroomSize,
                    SoLuongDat = item.PassedNum,
                    TiLe = tiLe
                });
            }
        }


        private void ExportReport()
        {
            Subject = SelectedSubject?.Name ?? "";
            Semester = SelectedSemester ?? "";
            AcademicYear = "2024-2025";
            FilterData();
        }

        private void ExportPdf() { /* TODO: Export PDF */ }

        private void ExportExcel() { /* TODO: Export Excel */ }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class ClassroomReportItem : INotifyPropertyChanged
    {
        private int _stt;
        private string _lop = "";
        private int _siSo;
        private int _soLuongDat;
        private string _tiLe = "";

        public int STT
        {
            get => _stt;
            set { _stt = value; OnPropertyChanged(nameof(STT)); }
        }

        public string Lop
        {
            get => _lop;
            set { _lop = value; OnPropertyChanged(nameof(Lop)); }
        }

        public int SiSo
        {
            get => _siSo;
            set { _siSo = value; OnPropertyChanged(nameof(SiSo)); }
        }

        public int SoLuongDat
        {
            get => _soLuongDat;
            set { _soLuongDat = value; OnPropertyChanged(nameof(SoLuongDat)); }
        }

        public string TiLe
        {
            get => _tiLe;
            set { _tiLe = value; OnPropertyChanged(nameof(TiLe)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
