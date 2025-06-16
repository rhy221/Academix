using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class GradeGroup
    {
        public string GradeName { get; set; }  
        public ObservableCollection<ClassGroup> Classes { get; set; } = new();
    }
}
