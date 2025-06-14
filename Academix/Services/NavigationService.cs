using Academix.Stores;
using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Services
{
    
    public class NavigationService
    {
        private NavigationStore _navigationStore;

        public NavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate(BaseViewModel currentViewModel)
        {
            _navigationStore.CurrentViewModel = currentViewModel;
        }


    }
}
