using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class ClassGroup
    {
        public string ClassName { get; set; }  
        public ObservableCollection<StudentDisplayModel> Students { get; set; } = new();
    }
}
