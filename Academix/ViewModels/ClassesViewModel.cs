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
    public class ClassesViewModel : INotifyPropertyChanged
    {
        private string selectedSemester;
        private string selectedSchoolYear;
        private string selectedTeacher;
        private Classroom selectedClassroom;

        private string newClassName;
        private string newTeacher;

        public ObservableCollection<Classroom> Classrooms { get; set; }
        public ObservableCollection<Classroom> FilteredClassrooms { get; set; }

        public string SelectedSemester
        {
            get => selectedSemester;
            set
            {
                selectedSemester = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSchoolYear
        {
            get => selectedSchoolYear;
            set
            {
                selectedSchoolYear = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTeacher
        {
            get => selectedTeacher;
            set
            {
                selectedTeacher = value;
                OnPropertyChanged();
            }
        }

        public Classroom SelectedClassroom
        {
            get => selectedClassroom;
            set
            {
                selectedClassroom = value;
                OnPropertyChanged();
                if (selectedClassroom != null)
                {
                    NewClassName = selectedClassroom.Name;
                    NewTeacher = selectedClassroom.TeacherName;
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

        public string NewTeacher
        {
            get => newTeacher;
            set
            {
                newTeacher = value;
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
            //Classrooms.Add(new Classroom("2024_10A1", 40, "HK1", "2024-2025", "Thầy A", new List<Student>()));
            //Classrooms.Add(new Classroom("2024_10A2", 38, "HK1", "2024-2025", "Thầy B", new List<Student>()));
            //Classrooms.Add(new Classroom("2024_11B1", 35, "HK2", "2023-2024", "Cô C", new List<Student>()));
            UpdateFiltered();

            SearchCommand = new RelayCommand((Action<object>)(obj => Search()));
            AddCommand = new RelayCommand((Action<object>)(obj => AddClass()));
            EditCommand = new RelayCommand((Action<object>)(obj => EditClass()));
            DeleteCommand = new RelayCommand((Action<object>)(obj => DeleteSelected()));
            ImportExportCommand = new RelayCommand((Action<object>)(obj => ImportExport()));

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
                (string.IsNullOrEmpty(SelectedTeacher) || c.TeacherName == SelectedTeacher)
            ).ToList();

            FilteredClassrooms.Clear();
            foreach (var cls in result)
                FilteredClassrooms.Add(cls);
        }

        private void AddClass()
        {
            if (string.IsNullOrWhiteSpace(NewClassName) || string.IsNullOrWhiteSpace(NewTeacher))
                return;

            string id = "TỰ THÊM";
            if (Classrooms.Any(c => c.ID == id))
                return;

            var newClass = new Classroom(
                id,
                0,
                "HỌC KỲ NÀO",
                DateTime.Now.Year + "-" + (DateTime.Now.Year + 1), // Năm học
                NewTeacher,
                new List<Student>()
            );

            Classrooms.Add(newClass);
            UpdateFiltered();
        }

        private void EditClass()
        {
            if (SelectedClassroom == null || string.IsNullOrWhiteSpace(NewTeacher))
                return;

            string id = SelectedClassroom.ID;
            var newClassroom = new Classroom(
                id,
                SelectedClassroom.Size,
                SelectedClassroom.Semester,
                SelectedClassroom.SchoolYear,
                NewTeacher,
                SelectedClassroom.Students);

            int index = Classrooms.IndexOf(SelectedClassroom);
            if (index >= 0)
            {
                Classrooms[index] = newClassroom;
                UpdateFiltered();
            }
        }

        private void DeleteSelected()
        {
            var classesToDelete = FilteredClassrooms.Where(c => c.IsSelected).ToList();
            foreach (var cls in classesToDelete)
            {
                Classrooms.Remove(cls);
            }
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
