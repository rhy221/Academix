using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Academix.Models
{
    public class Classroom : INotifyPropertyChanged
    {
        public string ID { get; set; } // NamHoc_TenLop

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private int size;
        public int Size
        {
            get => size;
            set
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged(nameof(Size));
                }
            }
        }

        private string semester;
        public string Semester
        {
            get => semester;
            set
            {
                if (semester != value)
                {
                    semester = value;
                    OnPropertyChanged(nameof(Semester));
                }
            }
        }

        private string schoolYear;
        public string SchoolYear
        {
            get => schoolYear;
            set
            {
                if (schoolYear != value)
                {
                    schoolYear = value;
                    OnPropertyChanged(nameof(SchoolYear));
                }
            }
        }

        private string teacherName;
        public string TeacherName
        {
            get => teacherName;
            set
            {
                if (teacherName != value)
                {
                    teacherName = value;
                    OnPropertyChanged(nameof(TeacherName));
                }
            }
        }

        private List<Student> _student;
        public List<Student> Students => new List<Student>(_student);

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public Classroom(string iD, string name, int size, string semester, string schoolYear, string teacherName, List<Student> students)
        {
            ID = iD;
            Name = name;
            Size = size;
            Semester = semester;
            SchoolYear = schoolYear;
            TeacherName = teacherName;
            _student = (students != null) ? new List<Student>(students) : new List<Student>();
        }

        public Classroom(Classroom other)
        {
            ID = other.ID;
            Name = other.Name;
            Size = other.Size;
            Semester = other.Semester;
            SchoolYear = other.SchoolYear;
            TeacherName = other.TeacherName;
            _student = new List<Student>(other.Students);
            IsSelected = other.IsSelected;
        }

        public Student this[int index]
        {
            get { return _student[index]; }
            set { _student[index] = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
