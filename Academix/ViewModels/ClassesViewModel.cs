using Academix.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        private string selectedSemester;
        private string selectedSchoolYear;
        private Classroom selectedClassroom;

        private string newClassName;
        private string selectedTeacherName;

        public ObservableCollection<Classroom> Classrooms { get; set; }
        public ObservableCollection<Classroom> FilteredClassrooms { get; set; }
        public ObservableCollection<string> Teachers { get; set; }

        private bool _isModifyTabVisible;
        public bool IsModifyTabVisible
        {
            get => _isModifyTabVisible;
            set
            {
                if (_isModifyTabVisible != value)
                {
                    _isModifyTabVisible = value;
                    OnPropertyChanged(nameof(IsModifyTabVisible));
                }
            }
        }


        public string SelectedSemester
        {
            get => selectedSemester;
            set
            {
                selectedSemester = value;
                OnPropertyChanged(nameof(SelectedSemester));
            }
        }

        public string SelectedSchoolYear
        {
            get => selectedSchoolYear;
            set
            {
                selectedSchoolYear = value;
                OnPropertyChanged(nameof(SelectedSchoolYear));
            }
        }

        public Classroom SelectedClassroom
        {
            get => selectedClassroom;
            set
            {
                if (selectedClassroom != value)
                {
                    selectedClassroom = value;
                    OnPropertyChanged(nameof(SelectedClassroom));

                    if (selectedClassroom != null)
                    {
                        NewClassName = selectedClassroom.ID.Split('_').Length > 1 ? selectedClassroom.ID.Split('_')[1] : selectedClassroom.ID;
                        SelectedTeacherName = selectedClassroom.TeacherName;
                        IsModifyTabVisible = true;
                    }
                }
            }
        }


        public string NewClassName
        {
            get => newClassName;
            set
            {
                newClassName = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTeacherName
        {
            get => selectedTeacherName;
            set
            {
                selectedTeacherName = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ImportExportCommand { get; }

        public ClassesViewModel()
        {
            Classrooms = new ObservableCollection<Classroom>();
            FilteredClassrooms = new ObservableCollection<Classroom>();

            // Thêm dữ liệu mẫu
            Classrooms.Add(new Classroom("2024_10A1", 40, "HK1", "2024-2025", "Thầy A", new List<Student>()));
            Classrooms.Add(new Classroom("2024_10A2", 38, "HK1", "2024-2025", "Thầy B", new List<Student>()));
            Classrooms.Add(new Classroom("2024_11B1", 35, "HK2", "2023-2024", "Cô C", new List<Student>()));
            UpdateFiltered();

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(AddClass);
            EditCommand = new RelayCommand(EditClass);
            DeleteCommand = new RelayCommand(DeleteSelected);
            ImportExportCommand = new RelayCommand(ImportExport);

            Teachers = new ObservableCollection<string>()
            {
                "Thầy An",
                "Thầy Bình",
                "Cô Cường",
                "Thầy Dinh",
                "Thầy Đạt"
            };
        }

        private void UpdateFiltered()
        {
            FilteredClassrooms.Clear();
            foreach (var cls in Classrooms)
                FilteredClassrooms.Add(cls);
        }

        private void Search()
        {
            var result = Classrooms.Where(c =>
                (string.IsNullOrEmpty(SelectedSemester) || c.Semester == SelectedSemester) &&
                (string.IsNullOrEmpty(SelectedSchoolYear) || c.SchoolYear == SelectedSchoolYear) &&
                (string.IsNullOrEmpty(SelectedTeacherName) || c.TeacherName == SelectedTeacherName)
            ).ToList();

            FilteredClassrooms.Clear();
            foreach (var cls in result)
                FilteredClassrooms.Add(cls);
        }

        private void AddClass()
        {
            if (string.IsNullOrWhiteSpace(NewClassName) || string.IsNullOrWhiteSpace(SelectedTeacherName))
                return;

            string yearPrefix = DateTime.Now.Year.ToString();
            string id = $"{yearPrefix}_{NewClassName.Replace(" ", "")}";


            var newClass = new Classroom(
                id,
                0,
                "HỌC KỲ NÀO",
                DateTime.Now.Year + "-" + (DateTime.Now.Year + 1),
                SelectedTeacherName,
                new List<Student>()
            );

            Classrooms.Add(newClass);
            UpdateFiltered();
        }

        private void EditClass()
        {
            if (SelectedClassroom == null || string.IsNullOrWhiteSpace(NewClassName) || string.IsNullOrWhiteSpace(SelectedTeacherName))
                return;

            string newID = SelectedClassroom.ID.Substring(0, 4) + "_" + NewClassName;
            if (Classrooms.Any(c => c.ID == newID && c != SelectedClassroom)) return;


            SelectedClassroom.ID = newID;
            SelectedClassroom.TeacherName = SelectedTeacherName;

            var index = FilteredClassrooms.IndexOf(SelectedClassroom);
            var original = Classrooms.FirstOrDefault(c => c.ID == SelectedClassroom.ID);
            if (original != null)
            {
                original.ID = newID;
                original.TeacherName = SelectedTeacherName;
            }
            UpdateFiltered();


        }

        private void DeleteSelected()
        {
            var classesToDelete = FilteredClassrooms.Where(c => c.IsSelected).ToList();
            foreach (var cls in classesToDelete)
            {
                Classrooms.Remove(cls);
            }
            SelectedClassroom = null;
            UpdateFiltered();

        }

        private void ImportExport()
        {
            // Nhập/xuất file CSV hoặc Excel (chưa triển khai)
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
