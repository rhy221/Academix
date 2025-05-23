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
            set => SetProperty(ref _isModifyTabVisible, value);
        }

        public string SelectedSemester
        {
            get => selectedSemester;
            set => SetProperty(ref selectedSemester, value);
        }

        public string SelectedSchoolYear
        {
            get => selectedSchoolYear;
            set => SetProperty(ref selectedSchoolYear, value);
        }

        public Classroom SelectedClassroom
        {
            get => selectedClassroom;
            set
            {
                if (SetProperty(ref selectedClassroom, value))
                {
                    if (selectedClassroom != null)
                    {
                        NewClassName = selectedClassroom.Name;
                        SelectedTeacherName = selectedClassroom.TeacherName;
                        IsModifyTabVisible = true;
                    }
                    else
                    {
                        NewClassName = string.Empty;
                        SelectedTeacherName = string.Empty;
                        IsModifyTabVisible = false;
                    }
                }
            }
        }

        public string NewClassName
        {
            get => newClassName;
            set => SetProperty(ref newClassName, value);
        }

        public string SelectedTeacherName
        {
            get => selectedTeacherName;
            set => SetProperty(ref selectedTeacherName, value);
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

            Classrooms.Add(new Classroom("2024_10A1", "10A1", 40, "HK1", "2024-2025", "Thầy A", new List<Student>()));
            Classrooms.Add(new Classroom("2024_10A2", "10A2", 38, "HK1", "2024-2025", "Thầy B", new List<Student>()));
            Classrooms.Add(new Classroom("2024_11B1", "11B1", 35, "HK2", "2023-2024", "Cô C", new List<Student>()));
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
                NewClassName,
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

            // Tìm index
            int index = Classrooms.IndexOf(SelectedClassroom);
            if (index < 0) return;

            // Tạo bản mới với thông tin cập nhật
            var updatedClassroom = new Classroom(
                SelectedClassroom.ID,
                NewClassName,
                SelectedClassroom.Size,
                SelectedClassroom.Semester,
                SelectedClassroom.SchoolYear,
                SelectedTeacherName,
                SelectedClassroom.Students
            );

            // Thay thế trong collection
            Classrooms[index] = updatedClassroom;

            // Cập nhật FilteredClassrooms (nếu dùng)
            UpdateFiltered();

            // Cập nhật SelectedClassroom trỏ tới bản mới
            SelectedClassroom = updatedClassroom;
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

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}