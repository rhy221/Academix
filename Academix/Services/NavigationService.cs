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

        private Stack<BaseViewModel> _viewModelStack = new Stack<BaseViewModel>();
        public void PushStack(BaseViewModel viewModel)
        {
            _viewModelStack.Push(viewModel);
        }
        public BaseViewModel PopStack()
        {
            if (_viewModelStack.Count > 0)
            {
                return _viewModelStack.Pop();
            }
            return null;
        }



        public void Navigate(BaseViewModel currentViewModel)
        {
            _navigationStore.CurrentViewModel = currentViewModel;
        }


    }
}
