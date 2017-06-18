using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HVTApp.Model.Wrappers
{
    public class ObservableWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private readonly List<WhoRised> _whoRisedEventPropertyChanged = new List<WhoRised>();

        protected virtual void OnPropertyChanged(object sender, string propertyName)
        {
            WhoRised whoRised = new WhoRised(sender, propertyName);
            if (!_whoRisedEventPropertyChanged.Contains(whoRised))
            {
                _whoRisedEventPropertyChanged.Add(whoRised);
                //OnPropertyChanged(propertyName);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                _whoRisedEventPropertyChanged.Remove(whoRised);
            }
        }
    }
}
