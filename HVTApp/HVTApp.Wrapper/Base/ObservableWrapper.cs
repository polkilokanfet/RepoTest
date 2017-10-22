using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HVTApp.Wrapper
{
    public class ObservableWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
