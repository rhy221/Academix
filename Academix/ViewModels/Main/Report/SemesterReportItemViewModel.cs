using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.Report
{
    public class SemesterReportItemViewModel:BaseViewModel
    {
        private Lop _class;

        public string ClassId => _class.Malop;

        public string ClassName
        {
            get => _class.Tenlop;
        }

        public int ClassSize
        {
            get => _class.Siso;
            set
            {
                _class.Siso = value;
                OnPropertyChanged(nameof(ClassSize));
            }
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        private double _passingRate;
        public double PassingRate
        {
            get => _passingRate;
            set
            {
                _passingRate = value;
                OnPropertyChanged(nameof(PassingRate));
            }
        }

        public SemesterReportItemViewModel(Lop cl, int count, double passingRate)
        {
            _class = cl;
            _count = count;
            _passingRate = passingRate;
        }
    }
}
