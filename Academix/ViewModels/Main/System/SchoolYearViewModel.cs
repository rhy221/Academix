using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.System
{
    public class SchoolYearViewModel: BaseViewModel
    {
        private Namhoc _schoolYear;
        public string Id => _schoolYear.Manamhoc;
        public string Name => _schoolYear.ToString();
        public int FirstYear
        {
            get => _schoolYear.Nam1;
            set
            {
                _schoolYear.Nam1 = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(FirstYear));
            }
        }

        public int SecondYear
        {
            get => _schoolYear.Nam2;
            set
            {
                _schoolYear.Nam2 = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(SecondYear));
            }
        }

        public SchoolYearViewModel(Namhoc schoolYear)
        {
            _schoolYear = schoolYear;
        }

        public override string ToString()
        {
            return $"{_schoolYear.Nam1}-{_schoolYear.Nam2}";
        }
    }
}
