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
        public string ID { get; } // NamHoc_TenLop
        public string Name => ID.Substring(4);
        public int Size { get; }
        public string Semester { get; }
        public string SchoolYear { get; }
        public List<Student> Students => new List<Student>(_student);

        private List<Student> _student;


        public Classroom(string iD, int size, string semester, string schoolYear, List<Student> students)
        {
            ID = iD;
            Size = size;
            Semester = semester;
            SchoolYear = schoolYear;
            _student = (students != null) ? new List<Student>(students) : new List<Student>();
        }

        public Classroom(Classroom other)
        {
            ID = other.ID;
            Size = other.Size;
            Semester = other.Semester;
            SchoolYear = other.SchoolYear;
            _student = new List<Student>(other.Students);
        }

        public Student this[int index]
        {
            get { return _student[index]; }
            set { _student[index] = value; }
        }

    }
}
