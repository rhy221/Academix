using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Academix.Models
{
    public class SubjectTranscript
    {
        public string ID { get; }
        public string Semester { get; }
        public Subject Subject => new Subject(_subject);
        public Classroom Class => new Classroom(_classroom);
        public IReadOnlyList<(string ID, string Name, Dictionary<string, List<float>> Scores)> ScoreTable => _scoreTable.AsReadOnly();

        private Subject _subject;
        private Classroom _classroom;
        private List<(string ID, string Name, Dictionary<string, List<float>> Scores)> _scoreTable;

        public SubjectTranscript(string iD, string semester, Subject subject, Classroom classroom)
        {
            ID = iD;
            Semester = semester;
            _subject = new Subject(subject);
            _classroom = new Classroom(classroom);
            _scoreTable = new List<(string, string, Dictionary<string, List<float>>)>();

        }

        public void SetSubject(Subject subject)
        {
            _subject = new Subject(subject);
        }

        public void SetClassroom(Classroom classroom)
        {
            _classroom = new Classroom(classroom);
        }

        public void SetScore(string id, string scorename, int no, float score)
        {
            _scoreTable.Find(item => item.ID == "id").Scores[scorename][no] = score;
        }


    }
}
