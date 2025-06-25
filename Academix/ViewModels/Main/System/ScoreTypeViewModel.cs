using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.System
{
    public class ScoreTypeViewModel:BaseViewModel
    {
        private Loaidiem _scoreType;

        public string Id => _scoreType.Maloaidiem;
        public string Name
        {
            get
            {
               return _scoreType.Tenloaidiem;
            }
            set
            {
                _scoreType.Tenloaidiem = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Multiplier
        {
            get
            {
                return _scoreType.Hesold;
            }
            set
            {
                _scoreType.Hesold = value;
                OnPropertyChanged(nameof(Multiplier));
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public ScoreTypeViewModel(Loaidiem scoreType)
        {
            _scoreType = scoreType;
        }

       
    }
}
