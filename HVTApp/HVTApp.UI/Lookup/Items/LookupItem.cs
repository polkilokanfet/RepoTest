using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using Prism.Mvvm;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupItem<TEntity> : BindableBase, ILookupItemNavigation<TEntity>, IComparable
        where TEntity : class, IBaseEntity
    {
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
        public void Refresh(TEntity entity)
        {
            Entity = entity;
            Refresh();
        }
        public void Refresh()
        {
            OnPropertyChanged(String.Empty);
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            return (T)Entity.GetType().GetProperty(propertyName)?.GetValue(Entity);
        }

        protected TLookup GetLookup<TLookup>([CallerMemberName] string propertyName = null)
            where TLookup : class 
        {
            var value = Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            if (value == null) return null;
            return (TLookup) Activator.CreateInstance(typeof(TLookup), value);
        }

        protected IEnumerable<TLookup> GetLookupEnum<TLookup>([CallerMemberName] string propertyName = null)
        {
            var members = (IEnumerable)Entity.GetType().GetProperty(propertyName).GetValue(Entity);
            foreach (var member in members)
            {
                yield return (TLookup) Activator.CreateInstance(typeof(TLookup), member);
            }
        }

        public override string ToString()
        {
            return Entity.ToString();
        }

        #region IComparable
        public virtual int CompareTo(object obj)
        {
            return ToString().CompareTo(obj.ToString());
        }

        #endregion

    }
}
