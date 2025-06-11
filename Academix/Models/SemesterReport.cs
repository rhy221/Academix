using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class SemesterReport: Report
    {
        public string Semester { get; }
        public string SchoolYear { get; }
        public List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)> Table => new List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)>(_table);

        private List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)> _table;

        public SemesterReport(string id, string semester, string schoolYear) : base(id)
        {
            Semester = semester;
            SchoolYear = schoolYear;
            _table = new List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)>();
        }

        public void setTable(string classID, int passedNum)
        {
            var item = _table.Find(match => match.ClassroomID == classID);
            item.PassedNum = passedNum;
            item.Percentage = Convert.ToSingle(Math.Round(passedNum * 1.0d / item.ClassroomSize));
        }
    }
}
