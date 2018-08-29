using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupItem<TEntity> : ILookupItemNavigation<TEntity>, INotifyPropertyChanged, IComparable
        where TEntity : class, IBaseEntity
    {
        protected LookupItem(TEntity entity)
        {
            Entity = entity;
            DisplayMember = Entity.ToString();
        }

        public Guid Id => GetValue<Guid>();

        public TEntity Entity { get; private set; }

        private string _displayMember;
        [Designation("Отображение")]
        public string DisplayMember
        {
            get { return _displayMember; }
            set { SetValue(ref _displayMember, value); }
        }

        /// <summary>
        /// Обновить Lookup
        /// </summary>
        /// <param name="entity">Основание для обновления.</param>
        public void Refresh(TEntity entity)
        {
            Entity = entity;
            RefreshLookups();
            Refresh();
        }

        public void Refresh()
        {
            OnPropertyChanged(String.Empty);
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
            //значение свойства в Entity
            var value = Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            if (Equals(value, null))
                return null;

            //если значение свойства уже содержится в словаре
            if (_lookups.ContainsKey(propertyName))
                return (TLookup)_lookups[propertyName];

            //добавление значения свойства в словарь
            var lookup = (TLookup) Activator.CreateInstance(typeof(TLookup), value);
            _lookups.Add(propertyName, lookup);
            return lookup;
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

        #region IComparable
        public virtual int CompareTo(object obj)
        {
            return ToString().CompareTo(obj.ToString());
        }

        public virtual async Task LoadOther(IUnitOfWork unitOfWork)
        {
            
        }

        #endregion

    }
}
