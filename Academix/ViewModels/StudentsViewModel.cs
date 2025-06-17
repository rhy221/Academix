using Academix.Models;
using Academix.Services;
using Academix.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Academix.ViewModels
{
    public class StudentsViewModel: BaseViewModel
    {
        private SchoolYearStore _schoolYearStore;
        private NavigationService _navigationService;

        public StudentsViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
        }

       

        public override string ToString()
        {
            return "Học sinh";
        }
    }
}
