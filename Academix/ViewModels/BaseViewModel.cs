using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IAsyncInitialization
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual async void Initialize()
        {
            throw new NotImplementedException();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
