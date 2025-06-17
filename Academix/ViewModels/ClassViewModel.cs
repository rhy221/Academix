using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    public class ClassViewModel: BaseViewModel
    {
        public Lop Class;

        public string Id => Class.Malop;

        public string ClassName
        {
            get => Class.Tenlop;
  
        }

        public int ClassSize
        {
            get => Class.Siso;
            set
            {
                Class.Siso = value;
                OnPropertyChanged(nameof(ClassSize));
            }
        }

        private string _gradeName;
        public string GradeName
        {
            get => _gradeName;
            set
            {
                _gradeName = value;
                OnPropertyChanged(nameof(GradeName));
            }
        }

        private string _schoolYearName;
        public string SchoolYearName
        {
            get => _schoolYearName;
            set
            {
                _schoolYearName = value;
                OnPropertyChanged(nameof(SchoolYearName));
            }
        }

        public ClassViewModel(Lop @class, string gradeName, string schoolYearName)
        {
            Class = @class;
            _gradeName = gradeName;
            _schoolYearName = schoolYearName;
        }
      
    }
}
