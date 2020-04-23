using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HVTApp.Model.Wrapper.Base
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
