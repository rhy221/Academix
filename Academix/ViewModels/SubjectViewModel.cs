using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    public class SubjectViewModel: BaseViewModel
    {
        private readonly Subject _subject;
       
        public string ID => _subject.ID;
        public string Name
        {
            get
            {
                return _subject.Name;
            }
            set
            {
                _subject.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int OralNum
        {
            get
            {
                return _subject.Scores[Subject.Oral];
            }
            set
            {
                _subject.SetNumberOfScore(Subject.Oral, value);
                OnPropertyChanged(nameof(OralNum));
            }
        }
        public int ShortNum
        {
            get
            {
                return _subject.Scores[Subject.Short];
            }
            set
            {
                _subject.SetNumberOfScore(Subject.Short, value);
                OnPropertyChanged(nameof(ShortNum));

            }
        }
        public int PeriodNum
        {
            get
            {
                return _subject.Scores[Subject.Period];
            }
            set
            {
                _subject.SetNumberOfScore(Subject.Period, value);
                OnPropertyChanged(nameof(PeriodNum));

            }
        }

        public SubjectViewModel(Subject subject)
        {
            _subject = subject;
        }

        public SubjectViewModel(SubjectViewModel other)
        {
            _subject = new Subject(other.ID, other.Name);
        }
    }
}
