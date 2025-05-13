using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class Classroom
    {
        public string ID { get; set; } // NamHoc_TenLop
        public string Name => ID.Substring(4);
        public int Size { get; set; }
        public string Semester { get; set; }
        public string SchoolYear { get; set; }
        public string TeacherName { get; set; }
        public List<Student> Students => new List<Student>(_student);

        private List<Student> _student;
        public bool IsSelected { get; set; }


        public Classroom(string iD, int size, string semester, string schoolYear, string teacherName, List<Student> students)
        {
            ID = iD;
            Size = size;
            Semester = semester;
            SchoolYear = schoolYear;
            TeacherName = teacherName;
            _student = (students != null) ? new List<Student>(students) : new List<Student>();
        }

        public Classroom(Classroom other)
        {
            ID = other.ID;
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

    }
}
