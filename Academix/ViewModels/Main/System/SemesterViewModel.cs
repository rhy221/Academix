using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels.Main.System
{

    public class SemesterViewModel:BaseViewModel
    {
        private Hocky _semester;

        public string Id => _semester.Mahocky;

        public string Name
        {
            get => _semester.Tenhocky;
            set
            {
                _semester.Tenhocky = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public SemesterViewModel(Hocky semester)
        {
            _semester = semester;
        }
    }
}
