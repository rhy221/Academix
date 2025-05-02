using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class SubjectReport: Report
    {
        public string Semester { get; }
        public string SchoolYear { get; }
        public Subject Subject => new Subject(_subject);
        public List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)> Table => new List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)>(_table);

        private Subject _subject;
        private List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)> _table;

        public SubjectReport(string id, string semester, string schoolYear, Subject subject): base(id)
        {
            Semester = semester;
            SchoolYear = schoolYear;
            _subject = new Subject(subject);
            _table = new List<(string ClassroomID, int ClassroomSize, int PassedNum, float Percentage)>();
        }

        public void SetSubject(Subject subject)
        {
            _subject = new Subject(Subject);
        }

        public void setTable(string classID, int passedNum)
        {
            var item = _table.Find(match => match.ClassroomID == classID);
            item.PassedNum = passedNum;
            item.Percentage = Convert.ToSingle(Math.Round(passedNum * 1.0d / item.ClassroomSize));
        }
        

       


    }
}
