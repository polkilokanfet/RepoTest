using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using Prism.Mvvm;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupItem<TEntity> : BindableBase, ILookupItemNavigation<TEntity>, IComparable, IId
        where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// Словарь сложных свойств
        /// </summary>
        private readonly Dictionary<string, object> _complexProperties = new Dictionary<string, object>();

        /// <summary>
        /// Словарь свойств-коллекций
        /// </summary>
        private readonly Dictionary<string, object> _collectionProperties = new Dictionary<string, object>();

        protected LookupItem(TEntity entity)
        {
            Entity = entity;
            DisplayMember = Entity.ToString();
        }

        public Guid Id => GetValue<Guid>();

        public TEntity Entity { get; private set; }

        [Designation("Отображение")]
        public string DisplayMember { get; set; }

        /// <summary>
        /// Обновить Lookup
        /// </summary>
        /// <param name="entity">Основание для обновления.</param>
        public void Refresh(TEntity entity = null)
        {
            if (entity != null)
            {
                Entity = entity;
                _complexProperties.Clear();
            }
            OnPropertyChanged(string.Empty);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return (T)Entity.GetType().GetProperty(propertyName)?.GetValue(Entity);
        }

        protected TLookup GetLookup<TLookup>([CallerMemberName] string propertyName = null)
            where TLookup : class 
        {
            //если свойство уже добавлено в словарь
            if (_complexProperties.ContainsKey(propertyName))
            {
                return (TLookup)_complexProperties[propertyName];
            }

            var value = Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            if (value == null) return null;
            var lookup = (TLookup) Activator.CreateInstance(typeof(TLookup), value);
            _complexProperties.Add(propertyName, lookup);
            return lookup;
        }

        protected IEnumerable<TLookup> GetLookupEnum<TLookup>([CallerMemberName] string propertyName = null)
        {
            if (_collectionProperties.ContainsKey(propertyName))
            {
                return (IEnumerable<TLookup>)_collectionProperties[propertyName];
            }

            var collection = new ObservableCollection<TLookup>();

            var members = (IEnumerable)Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            foreach (var member in members)
            {
                collection.Add((TLookup)Activator.CreateInstance(typeof(TLookup), member));
            }
            _collectionProperties.Add(propertyName, collection);
            return collection;
        }



        public override string ToString()
        {
            return Entity.ToString();
        }

        #region IComparable

        public virtual int CompareTo(object other)
        {
            return string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        #endregion

    }
}
