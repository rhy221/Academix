using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class StudentDisplayModel : BaseViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public double GPA1 { get; set; }
        public double GPA2 { get; set; }
        public string Status { get; set; }
        public int Index { get; set; }

    }
}
