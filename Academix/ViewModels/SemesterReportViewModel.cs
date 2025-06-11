using System;
using System.Collections.Generic;
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
        private string namHoc;
        private string selectedHocKy;
        private ObservableCollection<ClassDisplayData> filteredClasses;

        public string NamHoc
        {
            get => namHoc;
            set
            {
                if (namHoc != value)
                {
                    namHoc = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedHocKy
        {
            get => selectedHocKy;
            set
            {
                if (selectedHocKy != value)
                {
                    selectedHocKy = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isHocKyVisible;
        public bool IsHocKyVisible
        {
            get => isHocKyVisible;
            set
            {
                if (isHocKyVisible != value)
                {
                    isHocKyVisible = value;
                    OnPropertyChanged();
                }
            }
        }



        public ObservableCollection<string> HocKyList { get; set; }

        public ObservableCollection<SemesterReport> AllReports { get; set; }

        public ObservableCollection<ClassDisplayData> FilteredClasses
        {
            get => filteredClasses;
            set
            {
                if (filteredClasses != value)
                {
                    filteredClasses = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExportCommand { get; private set; }
        public ICommand ExportPdfCommand { get; private set; }
        public ICommand ExportExcelCommand { get; private set; }

        public SemesterReportViewModel()
        {
            NamHoc = "2024-2025";

            HocKyList = new ObservableCollection<string>
            {
                "Học kỳ 1",
                "Học kỳ 2"
            };

            SelectedHocKy = null;

            AllReports = new ObservableCollection<SemesterReport>();
            LoadDataFromDatabase();

            ExportCommand = new RelayCommand(ExportData);
            ExportPdfCommand = new RelayCommand(ExportPdf);
            ExportExcelCommand = new RelayCommand(ExportExcel);

            IsHocKyVisible = false;
        }

        private void ExportData()
        {
            if (!string.IsNullOrEmpty(NamHoc) && !string.IsNullOrEmpty(SelectedHocKy))
            {
                FilterData();
                IsHocKyVisible = true;
            }
            else
            {
                IsHocKyVisible = false;
            }
        }

        private void FilterData()
        {
            string hocKyShort = SelectedHocKy switch
            {
                "Học kỳ 1" => "HK1",
                "Học kỳ 2" => "HK2",
                _ => ""
            };

            var filtered = AllReports
                .Where(r => r.SchoolYear == NamHoc && r.Semester == hocKyShort)
                .SelectMany(r => r.Table.Select((row, index) => new ClassDisplayData
                {
                    STT = index + 1,
                    Lop = row.ClassroomID,
                    SiSo = row.ClassroomSize,
                    SoLuongDat = row.PassedNum
                }));

            FilteredClasses = new ObservableCollection<ClassDisplayData>(filtered);
        }

        private void ExportPdf()
        {
            // TODO: export PDF
        }

        private void ExportExcel()
        {
            // TODO: export Excel
        }

        private void LoadDataFromDatabase()
        {
            AllReports.Clear();

            var report1 = new SemesterReport("R1", "HK1", "2024-2025");
            AddTableEntry(report1, "10A1", 40, 35);
            AddTableEntry(report1, "10A2", 38, 30);
            AddTableEntry(report1, "10B1", 42, 38);

            var report2 = new SemesterReport("R2", "HK2", "2024-2025");
            AddTableEntry(report2, "10A1", 40, 37);
            AddTableEntry(report2, "10A2", 38, 33);
            AddTableEntry(report2, "10B1", 42, 40);

            AllReports.Add(report1);
            AllReports.Add(report2);
        }

        private void AddTableEntry(SemesterReport report, string classID, int classSize, int passedNum)
        {
            var tableField = typeof(SemesterReport).GetField("_table", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (tableField != null)
            {
                var list = (List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)>)tableField.GetValue(report);
                list.Add((classID, classSize, passedNum, passedNum * 100f / classSize));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ClassDisplayData : INotifyPropertyChanged
    {
        private int siSo;
        private int soLuongDat;

        public int STT { get; set; }
        public string Lop { get; set; }

        public int SiSo
        {
            get => siSo;
            set
            {
                if (siSo != value)
                {
                    siSo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TiLe));
                }
            }
        }

        public int SoLuongDat
        {
            get => soLuongDat;
            set
            {
                if (soLuongDat != value)
                {
                    soLuongDat = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TiLe));
                }
            }
        }

        public double TiLe => SiSo == 0 ? 0 : (double)SoLuongDat / SiSo * 100;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
