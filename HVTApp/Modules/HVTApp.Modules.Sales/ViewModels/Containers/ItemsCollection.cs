using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class ItemsCollection<T, TLookup> : IEnumerable<TLookup>, INotifyCollectionChanged
        where T : class, IBaseEntity
        where TLookup : LookupItem<T>
    {
        private readonly List<T> _items = new List<T>();
        private readonly ObservableCollection<TLookup> _lookups = new ObservableCollection<TLookup>();

        protected ItemsCollection(IEnumerable<T> items)
        {
            _lookups.CollectionChanged += (sender, args) => CollectionChanged?.Invoke(this, args);

            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Формирование из сущности отображаемую сущность.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract TLookup ConvertToLookup(T item);

        public void Add(T item)
        {
            if(_items.Contains(item))
                return;

            _items.Add(item);
            _lookups.Add(ConvertToLookup(item));
        }

        public void AddRange(IEnumerable<T> items) => items.ForEach(Add);

        public void Remove(T item)
        {
            if (_items.ContainsById(item))
                _items.RemoveById(item);

            if (_lookups.ContainsById(item))
                _lookups.RemoveById(item);
        }

        /// <summary>
        /// Очистка всех коллекций
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            _lookups.Clear();
        }

        #region Проброс свойств

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        IEnumerator IEnumerable.GetEnumerator() => _lookups.GetEnumerator();
        public IEnumerator<TLookup> GetEnumerator() => _lookups.GetEnumerator();
        

        #endregion
    }

    public class OffersCollection : ItemsCollection<Offer, OfferLookup>
    {
        public OffersCollection(IEnumerable<Offer> items) : base(items) { }

        protected override OfferLookup ConvertToLookup(Offer item)
        {
            return new OfferLookup(item);
        }
    }

    public class TendersCollection : ItemsCollection<Tender, TenderLookup>
    {
        public TendersCollection(IEnumerable<Tender> items) : base(items) { }

        protected override TenderLookup ConvertToLookup(Tender item)
        {
            return new TenderLookup(item);
        }
    }
}