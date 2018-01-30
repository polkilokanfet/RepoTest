using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupItem<TEntity> : ILookupItemNavigation<TEntity>, INotifyPropertyChanged
        where TEntity : class, IBaseEntity
    {
        protected LookupItem(TEntity entity)
        {
            Refresh(entity);
        }

        public Guid Id => GetValue<Guid>();

        public TEntity Entity { get; private set; }

        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { SetValue(ref _displayMember, value); }
        }

        public void Refresh(TEntity entity)
        {
            Entity = entity;
            RefreshLookups();
            OnPropertyChanged("");
        }

        protected abstract void RefreshLookups();

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return (T)Entity.GetType().GetProperty(propertyName)?.GetValue(Entity);
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        private readonly Dictionary<string, object> _lookups = new Dictionary<string, object>();
        protected TLookup GetLookup<TLookup>([CallerMemberName] string propertyName = null)
            where TLookup : class 
        {
            var value = Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            if (Equals(value, null))
                return null;

            if (_lookups.ContainsKey(propertyName))
            {
                return (TLookup)_lookups[propertyName];
            }
            else
            {
                var lookup = (TLookup) Activator.CreateInstance(typeof(TLookup), value);
                _lookups.Add(propertyName, lookup);
                return lookup;
            }
        }

        public override string ToString()
        {
            return Entity.ToString();
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
