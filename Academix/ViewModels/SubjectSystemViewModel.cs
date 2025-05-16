using Academix.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
namespace Academix.ViewModels
{
    public class SubjectSystemViewModel: BaseViewModel
    {
        private readonly ObservableCollection<SubjectViewModel> _subjects;
        public IEnumerable<SubjectViewModel> Subjects => _subjects;
        public int Size => _subjects.Count;
        private SubjectViewModel _currentModifyingSubject;
        public string ModifyID => _currentModifyingSubject.ID;
        public string ModifyName
        {
            get
            {
                return _currentModifyingSubject.Name;
            }
            set
            {
                _currentModifyingSubject.Name = value;
                OnPropertyChanged(nameof(ModifyName));
            }
        }
        public int ModifyOralNum
        {
            get
            {
                return _currentModifyingSubject.OralNum;

            }
            set
            {
                _currentModifyingSubject.OralNum = value;
                OnPropertyChanged(nameof(ModifyOralNum));

            }
        }
        public int ModifyShortNum
        {
            get
            {
                return _currentModifyingSubject.ShortNum;
            }
            set
            {
                _currentModifyingSubject.ShortNum = value;
                OnPropertyChanged(nameof(ModifyShortNum));

            }
        }
        public int ModifyPeriodNum
        {
            get
            {
                return _currentModifyingSubject.PeriodNum;
            }
            set
            {
                _currentModifyingSubject.PeriodNum = value;
                OnPropertyChanged(nameof(ModifyPeriodNum));

            }
        }
        private SubjectViewModel _addNewSubject;
        public string NewName
        {
            get
            {
                return _addNewSubject.Name;
            }
            set
            {
                _addNewSubject.Name = value;
            }
        }
        public int NewOralNum
        {
            get
            {
                return _addNewSubject.OralNum;
            }
            set
            {
                _addNewSubject.OralNum = value;
            }
        }
        public int NewShortNum
        {
            get
            {
                return _addNewSubject.ShortNum;
            }
            set
            {
                _addNewSubject.ShortNum = value;
            }
        }
        public int NewPeriodNum
        {
            get
            {
                return _addNewSubject.PeriodNum;
            }
            set
            {
                _addNewSubject.PeriodNum = value;
            }
        }

        private SubjectViewModel _selectedSubject;
        public SubjectViewModel SelectedSubject
        {
            get
            {
                return _selectedSubject;
            }
            set
            {
                _selectedSubject = value;
                OnPropertyChanged(nameof(SelectedSubject));

                if(_selectedSubject != null)
                {
                    ShowSelectedSubject(_selectedSubject);
                }
            }
        }

        public ICommand ModifyCommand { get; }
        public ICommand AddNewCommand { get; }
        //public ICommand RestoreCommand { get; }
        public ICommand SaveCommand { get; }

        public SubjectSystemViewModel()
        {
            _subjects = new ObservableCollection<SubjectViewModel>();
            _addNewSubject = new SubjectViewModel(new Subject("*", ""));
            _currentModifyingSubject = new SubjectViewModel(new Subject("",""));
            _selectedSubject = null;
            AddNewCommand = new RelayCommand(AddNewSubject);
            ModifyCommand = new RelayCommand(ModifySubject);
            SaveCommand = new RelayCommand(SaveData);
            _subjects.Add(new SubjectViewModel(new Subject("fdf", "toan")));
            _subjects.Add(new SubjectViewModel(new Subject("fdf", "toan")));
            _subjects.Add(new SubjectViewModel(new Subject("fdf", "toan")));
            _subjects.Add(new SubjectViewModel(new Subject("fdf", "toan")));

        }

        private void AddNewSubject()
        {
            if(!string.IsNullOrWhiteSpace(_addNewSubject.Name))
                _subjects.Add(new SubjectViewModel(new Subject(Guid.NewGuid().ToString(), _addNewSubject.Name, new Dictionary<string, int>() { [Subject.Oral] = NewOralNum, [Subject.Short] = NewShortNum, [Subject.Period] = NewPeriodNum})));
        }

        private void ModifySubject()
        {
            if(!string.IsNullOrWhiteSpace(_selectedSubject.Name))
            {
                _selectedSubject.Name = _currentModifyingSubject.Name;
                _selectedSubject.OralNum = _currentModifyingSubject.OralNum;
                _selectedSubject.ShortNum = _currentModifyingSubject.ShortNum;
                _selectedSubject.PeriodNum = _currentModifyingSubject.PeriodNum;
            }
           
        }

        private void ShowSelectedSubject(SubjectViewModel subject)
        {
            _currentModifyingSubject = new SubjectViewModel(new Subject(subject.ID, subject.Name, new Dictionary<string, int>() { [Subject.Oral] = subject.OralNum, [Subject.Short] = subject.ShortNum, [Subject.Period] = subject.PeriodNum}));
            OnPropertyChanged(nameof(ModifyID));
            OnPropertyChanged(nameof(ModifyName));
            OnPropertyChanged(nameof(ModifyOralNum));
            OnPropertyChanged(nameof(ModifyShortNum));
            OnPropertyChanged(nameof(ModifyPeriodNum));
        }

        private void SaveData()
        {

        }

    }
}
