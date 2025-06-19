using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models;
using System.Globalization;

namespace Academix.ViewModels
{
    public class StudentDisplayViewModel : BaseViewModel
    {

        public Hocsinh Student;

        public string Id
        {
            get => Student.Mahs;
        }

        public string Name
        {
            get => Student.Hoten;
            set
            {
                Student.Hoten = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Gender
        {
            get => Student.Gioitinh;
            set
            {
                Student.Gioitinh = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public string DateOfBirth
        {
            get => Student.Ngaysinh.ToString("dd/MM/yyyy");
            set
            {
                Student.Ngaysinh = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
     
        public string ClassName
        {
            get
            {
                if (Student.CtLops.Count > 0)
                {
                    return Student.CtLops.LastOrDefault().MalopNavigation.Tenlop;
                }
                return "Không có thông tin";
            }
            set
            {

                if (Student.CtLops.Count > 0)
                    Student.CtLops.First().MalopNavigation.Tenlop = value;
                OnPropertyChanged(nameof(ClassName));
            }
        }

        public double GPA1
        {
            get
            {
                if (Student.CtLops.Count > 0)
                {
                    return Math.Round(Student.CtLops.FirstOrDefault().Dtbhk, 2);
                }
                   
                return 0;
            }
        }

        public double GPA2
        {
            get
            {
                if (Student.CtLops.Count > 1)
                    return Math.Round(Student.CtLops.LastOrDefault().Dtbhk, 2);
                return 0;
            }
        }


        public StudentDisplayViewModel(Hocsinh student)
        {
            Student = student;
        }


    }
}
