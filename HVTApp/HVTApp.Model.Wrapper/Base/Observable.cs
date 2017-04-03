using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HVTApp.Model.Wrapper.Annotations;

namespace HVTApp.Model.Wrapper
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly List<WhoRised> _whoRisedEventPropertyChanged = new List<WhoRised>();

        protected void OnPropertyChanged(object sender, string propertyName)
        {
            WhoRised whoRised = new WhoRised(sender, propertyName);
            if (!_whoRisedEventPropertyChanged.Contains(whoRised))
            {
                _whoRisedEventPropertyChanged.Add(whoRised);
                OnPropertyChanged(propertyName);
                _whoRisedEventPropertyChanged.Remove(whoRised);
            }
        }

    }
}
