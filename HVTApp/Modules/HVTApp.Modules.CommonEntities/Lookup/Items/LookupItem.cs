using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;

namespace HVTApp.UI.Lookup
{
    public class LookupItem : ILookupItem, INotifyPropertyChanged
    {
        private string _displayMember;
        public Guid Id { get; set; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (Equals(_displayMember, value)) return;
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return DisplayMember;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public interface ILookupItem
    {
        string DisplayMember { get; set; }
        Guid Id { get; set; }
    }
}
