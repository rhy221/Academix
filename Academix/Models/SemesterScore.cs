using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models.Enum;

namespace Academix.Models
{
    public class SemesterScore
    {
        public Semester Semester { get; set; }
        public List<SubjectScore> Subjects { get; set; } = new();
    }

}
