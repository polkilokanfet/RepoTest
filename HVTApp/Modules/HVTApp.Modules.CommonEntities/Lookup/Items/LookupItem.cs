using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public class LookupItem : ILookupItem, INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { SetSimpleProperty(ref _displayMember, value); }
        }

        public void Refresh(IBaseEntity entity)
        {
            var props = entity.GetType().GetProperties().Where(p => !p.PropertyType.IsClass || 
                                                                     p.PropertyType == typeof(String) ||
                                                                     p.PropertyType == typeof(DateTime));
            foreach (var propertyInfo in props)
            {
                var propsName = propertyInfo.Name;
                var value = propertyInfo.GetValue(entity);
                this.GetType().GetProperty(propsName)?.SetValue(this, value);
            }
        }

        protected void SetSimpleProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
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
}
