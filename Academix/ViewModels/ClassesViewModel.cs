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
        private Classroom selectedClassroom;

        private string newClassName;

        private string _selectedSearchTeacher;
        private string _selectedAddTeacher;
        private string _selectedSearchGrade;
        private string _selectedSearchClass;

        private string editClassName;
        private string selectedEditTeacher;

        public ObservableCollection<Classroom> Classrooms { get; set; }
        public ObservableCollection<Classroom> FilteredClassrooms { get; set; }
        public ObservableCollection<string> Teachers { get; set; }
        public ObservableCollection<string> Grades { get; set; }
        public ObservableCollection<string> ClassNames { get; set; }

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
                        EditClassName = selectedClassroom.Name;
                        SelectedEditTeacher = selectedClassroom.TeacherName;
                        IsModifyTabVisible = true;
                    }
                    else
                    {
                        EditClassName = string.Empty;
                        SelectedEditTeacher = string.Empty;
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

        public string SelectedSearchTeacher
        {
            get => _selectedSearchTeacher;
            set => SetProperty(ref _selectedSearchTeacher, value);
        }

        public string SelectedAddTeacher
        {
            get => _selectedAddTeacher;
            set => SetProperty(ref _selectedAddTeacher, value);
        }

        public string SelectedSearchGrade
        {
            get => _selectedSearchGrade;
            set => SetProperty(ref _selectedSearchGrade, value);
        }

        public string SelectedSearchClass
        {
            get => _selectedSearchClass;
            set => SetProperty(ref _selectedSearchClass, value);
        }

        public string EditClassName
        {
            get => editClassName;
            set => SetProperty(ref editClassName, value);
        }

        public string SelectedEditTeacher
        {
            get => selectedEditTeacher;
            set => SetProperty(ref selectedEditTeacher, value);
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
                "Thầy Đạt",
                "Cô Nhi",
                "Cô Ngân",
                "Thây Tân"
            };

            Grades = new ObservableCollection<string>()
            {
                "10", "11", "12"
            };

            ClassNames = new ObservableCollection<string>()
            {
                "10A1", "10A2", "10A3", "10A4",
                "11A1", "11A2", "11A3",
                "12A1", "12A2"
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
                (string.IsNullOrWhiteSpace(SelectedSearchTeacher) || c.TeacherName == SelectedSearchTeacher) &&
                (string.IsNullOrWhiteSpace(SelectedSearchGrade) || c.Name.StartsWith(SelectedSearchGrade)) &&
                (string.IsNullOrWhiteSpace(SelectedSearchClass) || c.Name.Contains(SelectedSearchClass))
            ).ToList();

            FilteredClassrooms.Clear();
            foreach (var cls in result)
                FilteredClassrooms.Add(cls);
        }

        private void AddClass()
        {
            if (string.IsNullOrWhiteSpace(NewClassName) || string.IsNullOrWhiteSpace(SelectedAddTeacher))
                return;

            string yearPrefix = DateTime.Now.Year.ToString();
            string id = $"{yearPrefix}_{NewClassName.Replace(" ", "")}";

            if (Classrooms.Any(c => c.ID == id))
                return;

            var newClass = new Classroom(
                id,
                NewClassName,
                0,
                "HK1", 
                DateTime.Now.Year + "-" + (DateTime.Now.Year + 1),
                SelectedAddTeacher,
                new List<Student>()
            );

            Classrooms.Add(newClass);
            UpdateFiltered();


            NewClassName = string.Empty;
            SelectedAddTeacher = null;
        }

        private void EditClass()
        {
            if (SelectedClassroom == null || string.IsNullOrWhiteSpace(EditClassName) || string.IsNullOrWhiteSpace(SelectedEditTeacher))
                return;

            SelectedClassroom.Name = EditClassName;
            SelectedClassroom.TeacherName = SelectedEditTeacher;

            SelectedClassroom.OnPropertyChanged(nameof(Classroom.Name));
            SelectedClassroom.OnPropertyChanged(nameof(Classroom.TeacherName));

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
            // TODO: triển khai nhập/xuất CSV hoặc Excel
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

    }
}
