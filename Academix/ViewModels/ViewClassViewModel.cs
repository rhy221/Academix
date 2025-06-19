using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Academix.Models;
using Academix.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Academix.ViewModels
{
    public class ViewClassViewModel: BaseViewModel
    {
        private ObservableCollection<StudentViewModel> _students;
        public ObservableCollection<StudentViewModel> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public string ClassName => _class.Tenlop;
        public int ClassSize => _class.Siso;
        public String SchoolYearName => _class.ManamhocNavigation.ToString();
        public ICommand BackCommand { get; }
        private NavigationService _navigationService;
        private Lop _class;

        public ViewClassViewModel (NavigationService navigationService, Lop @class)
        {
            _class = @class;
            _navigationService = navigationService;
            BackCommand = new RelayCommand(Back);
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

       private void Back()
        {
            BaseViewModel viewModel = _navigationService.PopStack();
           if ( viewModel != null)
            {
                _navigationService.Navigate(viewModel);
            }
        }

        public override string ToString()
        {
            return "Lớp >> Thông tin lớp" ;
        }

        private async Task LoadDataAsync()
        {
            using(var context = new QuanlyhocsinhContext())
            {

               List<Hocsinh> students = await context.Hocsinhs
                                            .Where(hs => hs.CtLops.Any(ct => ct.Malop == _class.Malop))
                                            .ToListAsync();

                                                        
                ObservableCollection<StudentViewModel> studentViewModels = new ObservableCollection<StudentViewModel>();
                foreach(Hocsinh student in students)
                {
                    studentViewModels.Add(new StudentViewModel(student));
                }

                Students = studentViewModels;
            }
        }

        
    }
}
