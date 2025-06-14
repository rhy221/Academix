using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("LOP")]
    public class Classroom
    {
        [Key]
        public string ID { get; set; } // NamHoc_TenLop
        public string Name => ID.Substring(4);
        [Column("SISO")]
        public int Size { get; set; }
        [ForeignKey("MAKHOI")]
        public string GradeId { get; set; }
        [ForeignKey("MANAMHOC")]
        public string SchoolYearId { get; set; }
        private List<Student> _student;
        public List<Student> Students => _student;


        public Classroom(string iD, int size, string gradeId, string schoolYearId, List<Student> students)
        {
            ID = iD;
            Size = size;
            GradeId = gradeId;
            SchoolYearId = schoolYearId;
            _student = (students != null) ? new List<Student>(students) : new List<Student>();
        }

        public Classroom(Classroom other)
        {
            ID = other.ID;
            Size = other.Size;
            GradeId = other.GradeId;
            SchoolYearId = other.SchoolYearId;
            _student = new List<Student>(other.Students);
        }

        public Student this[int index]
        {
            get { return _student[index]; }
            set { _student[index] = value; }
        }

    }
}
