using Academix.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Stores
{
    public class SchoolYearStore
    {
        private Namhoc _selectedSchoolYear;
        public Namhoc SelectedSchoolYear
        {
            get => _selectedSchoolYear;
            set
            {
                _selectedSchoolYear = value;
                OnSelectedSchoolYearChange();
            }
        }

        private List<Namhoc> _schoolyears;
        
        public List<Namhoc> SchoolYears
        {
            get => _schoolyears;
            set
            {
                _schoolyears = value;
                OnSchoolYearsChange();
                
            }
        }

        public event Action SelectedSchoolYearChanged;
        
        private void OnSelectedSchoolYearChange()
        {
            SelectedSchoolYearChanged?.Invoke();
        }

        public event Action SchoolYearsChanged;

        private void OnSchoolYearsChange()
        {
            SchoolYearsChanged?.Invoke();
        }
    }
}
