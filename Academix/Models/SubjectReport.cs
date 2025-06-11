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
            _subject = new Subject(subject);
        }


        public void setTable(string classID, int passedNum)
        {
            int index = _table.FindIndex(match => match.ClassroomID == classID);
            if (index >= 0)
            {
                var oldItem = _table[index];
                float percentage = (oldItem.ClassroomSize > 0) ? (float)Math.Round((double)passedNum / oldItem.ClassroomSize, 2) : 0f;
                _table[index] = (oldItem.ClassroomID, oldItem.ClassroomSize, passedNum, percentage);
            }
            else
            {
                _table.Add((classID, 0, passedNum, 0f));
            }
        }






    }
}
