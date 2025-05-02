using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Academix.ViewModels
{
    public class SystemParametersViewModel: BaseViewModel
    {
        private int _minimumAge;
        public int MinimumAge
        {
            get
            {
                return _minimumAge;
            }
            set
            {
                _minimumAge = value;
                OnPropertyChanged(nameof(MinimumAge));
            }
        }

        private int _maximumAge;
        public int MaximumAge
        {
            get
            {
                return _maximumAge;
            }
            set
            {
                _maximumAge = value;
                OnPropertyChanged(nameof(MaximumAge));
            }
        }

        private int _maximumClasssize;
        public int MaximumClassize
        {
            get
            {
                return _maximumClasssize;
            }
            set
            {
                _maximumClasssize = value;
                OnPropertyChanged(nameof(MaximumClassize));
            }
        }

        private float _minimumScore;
        public float MinimumScore
        {
            get
            {
                return _minimumScore;
            }
            set
            {
                _minimumScore = value;
                OnPropertyChanged(nameof(MinimumScore));
            }
        }

        private float _maximumScore;
        public float MaximumScore
        {
            get
            {
                return _maximumScore;
            }
            set
            {
                _maximumScore = value;
                OnPropertyChanged(nameof(MaximumScore));
            }
        }

        private float _passingGrade;
        public float PassingGrade
        {
            get
            {
                return _passingGrade;
            }
            set
            {
                _passingGrade = value;
                OnPropertyChanged(nameof(PassingGrade));
            }
        }

        private float _subjectPassingGrade;
        public float SubjectPassingGrade
        {
            get
            {
                return _subjectPassingGrade;
            }
            set
            {
                _subjectPassingGrade = value;
                OnPropertyChanged(nameof(SubjectPassingGrade));
            }
        }

        //public ICommand RestoreCommand { get; }
        public ICommand SaveCommand { get; }

        public SystemParametersViewModel()
        {
            SaveCommand = new RelayCommand(SaveData);
            _minimumAge = 15;
            _maximumAge = 20;
            _maximumClasssize = 40;
            _minimumScore = 0f;
            _maximumScore = 10f;
            _passingGrade = 5f;
            _subjectPassingGrade = 5f;
        }

        private void SaveData()
        {

        }

    }
}
