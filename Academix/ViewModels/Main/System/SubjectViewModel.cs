using Academix.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Academix.ViewModels.Main.System
{
    public class SubjectViewModel: BaseViewModel
    {
        private readonly Monhoc _subject;
       
        public string Id => _subject.Mamh;
        public string Name
        {
            get
            {
                return _subject.Tenmh;
            }
            set
            {
                _subject.Tenmh = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Multiplier {
            get
            {
                return _subject.Heso;
            }
            set
            {
                _subject.Heso = value;
                OnPropertyChanged(nameof(Multiplier));
            }
        }

        private ObservableCollection<Loaidiem> _scoreTypes;
        private string _scoreTypesString;
        public ObservableCollection<Loaidiem> ScoreTypes
        {
            get
            {
                return _scoreTypes;
            }
            set
            {
                _scoreTypes = value;

                string scoreTypesString = "";
                if(!_scoreTypes.IsNullOrEmpty())
                    scoreTypesString += _scoreTypes[0].Tenloaidiem;
                for (int i = 1; i < _scoreTypes.Count; i++)
                {

                    scoreTypesString += ", " + _scoreTypes[i].Tenloaidiem;
                }
                ScoreTypesString = scoreTypesString;
                OnPropertyChanged(nameof(ScoreTypes));
            }
        }

        public string ScoreTypesString
        {
            get
            {
                return _scoreTypesString;
            }
            set
            {
                _scoreTypesString = value;
                OnPropertyChanged(nameof(ScoreTypesString));
            }
        }

        public SubjectViewModel(Monhoc subject)
        {
            _subject = subject;
            _scoreTypes = new ObservableCollection<Loaidiem>();
        }

        public SubjectViewModel(SubjectViewModel other)
        {
            _subject = new Monhoc();
            _subject.Mamh = other._subject.Mamh;
            _subject.Tenmh = other._subject.Tenmh;
            _subject.Heso = other._subject.Heso;
            _subject.Maloaidiems = other._subject.Maloaidiems;
            _subject.Bangdiemmonhocs = other._subject.Bangdiemmonhocs;
            _subject.Bctongketmons = other._subject.Bctongketmons;
            _scoreTypes = new ObservableCollection<Loaidiem>(other.ScoreTypes);
        }

    }
}
