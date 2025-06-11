using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class Subject
    {
        public static string Oral = "M";
        public static string Short = "15P";
        public static string Period = "1T";
        public static string Final = "HK";

        public string ID { get; }
        public string Name { get; set; }
        public IReadOnlyDictionary<string, int> Scores => _scores.AsReadOnly();

        private Dictionary<string, int> _scores;

        public Subject(string iD, string name)
        {
            ID = iD;
            Name = name;
            _scores = new Dictionary<string, int>() { [Oral] = 3, [Short] = 3, [Period] = 4, [Final] = 1 };
        }

        public Subject(string iD, string name, Dictionary<string, int> score)
        {
            ID = iD;
            Name = name;
            _scores = score;
        }

        public Subject(Subject other)
        {
            ID = other.ID;
            Name = other.Name;
            _scores = new Dictionary<string, int>(other.Scores);
        }

        public int GetNumberOfScore(string key)
        {
            return _scores[key];
        }

        public void SetNumberOfScore(string key, int number)
        {
            _scores[key] = number;
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
