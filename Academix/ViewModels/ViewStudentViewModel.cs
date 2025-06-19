using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Academix.Models;
using Academix.Services;
using System.Globalization;

namespace Academix.ViewModels
{
    public class ViewStudentViewModel:BaseViewModel
    {
        
        public ICommand BackCommand { get; }
        private Hocsinh _student;
        private NavigationService _navigationService;

        public string GradeName
        {
            get
            {
                if(_student.CtLops.Count > 0)
                {
                    return _student.CtLops.LastOrDefault().MalopNavigation.MakhoiNavigation.Tenkhoi;
                }
                return "";
            }
        }
        public string ClassName
        {
            get
            {
                if (_student.CtLops.Count > 0)
                {
                    return _student.CtLops.LastOrDefault().MalopNavigation.Tenlop;
                }
                return "";
            }
        }

        public string Id => _student.Mahs;
        public string Name => _student.Hoten;
        public string Gender => _student.Gioitinh;
        public string DateOfBirth => _student.Ngaysinh.ToString("dd/MM/yyyy");
        public string Province
        {
            get
            {
                if (_address.Count() > 0)
                    return _address[0];
                return "";

            }
        }
        public string District
        {
            get
            {
                if (_address.Count() > 1)
                    return _address[1];
                return "";

            }
        }
        public string Commune
        {
            get
            {
                if (_address.Count() > 2)
                    return _address[2];
                return "";

            }
        }
        private string[] _address;


        public ViewStudentViewModel(NavigationService navigationService,Hocsinh student)
        {
            _student = student;
            _navigationService = navigationService;
            _address = _student.Diachi.Split("_");
            BackCommand = new RelayCommand(Back);
            
        }

        public string Email => _student.Email;
        

      
        private void Back()
        {
            BaseViewModel viewModel = _navigationService.PopStack();
            if (viewModel != null)
                _navigationService.Navigate(viewModel);

        }
        public override string ToString()
        {
            return "Học sinh >> Xem thông tin học sinh ";
        }
    }
}
