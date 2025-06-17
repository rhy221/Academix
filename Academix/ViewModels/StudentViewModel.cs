using Academix.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    public class StudentViewModel:BaseViewModel
    {

        private Hocsinh _student;

        public string Id {
            get => _student.Mahs;
        }

        public string Name {
            get => _student.Hoten;
            set
            {
                _student.Hoten = value;
                OnPropertyChanged(nameof(Name));
            } 
        }

        public string Gender
        {
            get => _student.Gioitinh;
            set
            {
                _student.Gioitinh = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public string DateOfBirth 
        {
            get => _student.Ngaysinh.ToString("dd/MM/yyyy");
            set
            {
                _student.Ngaysinh = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                OnPropertyChanged(nameof(DateOfBirth));
            } 
        }

        public string Address {
            get => _student.Diachi;
            set
            {
                _student.Diachi = value;
                OnPropertyChanged(nameof(Address));
            } 
        } 

        public string Email {
            get => _student.Email;
             set
            {
                _student.Email = value;
                OnPropertyChanged(nameof(Email));

            } 
        }

        public StudentViewModel(Hocsinh student)
        {
            _student = student;
        }
    }
}
