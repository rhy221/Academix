using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models;

namespace Academix.Models
{
    public class StudentDisplayModel : BaseViewModel
    {
        public Student Student { get; set; }
        public string ClassName { get; set; }
        public double GPA1 { get; set; }
        public double GPA2 { get; set; }
        public string Status { get; set; }
        public int Index { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public StudentDisplayModel(Student student, string className, double gpa1, double gpa2, string status, int index, bool isselected)
        {
            Student = student;
            ClassName = className;
            GPA1 = gpa1;
            GPA2 = gpa2;
            Status = status;
            Index = index;
            IsSelected = isselected;

        }
        public string ID => Student?.ID;
        public string Name => Student?.Name;
        public string Gender => (Student != null && Student.Gender) ? "Nam" : "Nữ";
        public byte[] Images => Student?.Images;
        public DateTime DateOfBirth => (DateTime)(Student?.DateOfBirth);
    }
}
