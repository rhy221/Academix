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
    public class SubjectReportViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> SemesterList { get; } = new ObservableCollection<string> { "HK1", "HK2", "HK3" };
        public ObservableCollection<string> AcademicYearList { get; } = new ObservableCollection<string> { "2022-2023", "2023-2024", "2024-2025" };
        public ObservableCollection<Subject> SubjectList { get; } = new ObservableCollection<Subject>();

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
            SubjectList.Add(new Subject("Math", "Toán học"));
            SubjectList.Add(new Subject("Phys", "Vật lý"));
            SubjectList.Add(new Subject("Chem", "Hóa học"));

            UpdateLabels();

            ExportReportCommand = new RelayCommand(() => ExportReport());
            ExportPdfCommand = new RelayCommand(() => ExportPdf());
            ExportExcelCommand = new RelayCommand(() => ExportExcel());

            LoadFakeReports();

            // Không gọi FilterData() ở đây, dữ liệu chỉ load khi bấm ExportReport
        }

        private void UpdateLabels()
        {
            Subject = SelectedSubject?.Name ?? "";
            Semester = SelectedSemester ?? "";
            AcademicYear = SelectedAcademicYear ?? "";
        }

        private void LoadFakeReports()
        {
            var math = SubjectList.First(s => s.ID == "Math");

            var report = new SubjectReport("R001", "HK1", "2024-2025", math);
            report.setTable("10A1", 25);
            report.setTable("10A2", 20);
            AllReports.Add(report);

            var report2 = new SubjectReport("R002", "HK2", "2024-2025", math);
            report2.setTable("10A1", 22);
            report2.setTable("10A3", 23);
            AllReports.Add(report2);
        }


        private void FilterData()
        {
            if (SelectedSubject == null || SelectedSemester == null || SelectedAcademicYear == null)
            {
                ClassReportList.Clear();
                return;
            }

            var report = AllReports.FirstOrDefault(r =>
                r.Subject.ID == SelectedSubject.ID &&
                r.Semester == SelectedSemester &&
                r.SchoolYear == SelectedAcademicYear);

            ClassReportList.Clear();

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
            UpdateLabels();
            FilterData();
        }

        private void ExportPdf()
        {
            // TODO: Thêm code xuất PDF nếu cần
        }

        private void ExportExcel()
        {
            // TODO: Thêm code xuất Excel nếu cần
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class ClassroomReportItem : INotifyPropertyChanged
    {
        private int _stt;
        private string _lop;
        private int _siSo;
        private int _soLuongDat;
        private string _tiLe;

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
