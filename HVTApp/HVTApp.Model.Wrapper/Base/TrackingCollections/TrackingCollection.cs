using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public class TrackingCollection<T> : ObservableCollection<T>, IValidatableChangeTracking
    {
        /// <summary>
        /// Начальный (оригинальный) список членов коллекции.
        /// </summary>
        private IList<T> _originalCollection;

        private readonly ObservableCollection<T> _addedItems;
        private readonly ObservableCollection<T> _removedItems;

        public TrackingCollection(IEnumerable<T> items) : base(items)
        {
            //фиксируем то, что мы имели изначально.
            _originalCollection = this.ToList();

            _addedItems = new ObservableCollection<T>();
            _removedItems = new ObservableCollection<T>();

            AddedItems = new ReadOnlyObservableCollection<T>(_addedItems);
            RemovedItems = new ReadOnlyObservableCollection<T>(_removedItems);
        }

        public ReadOnlyObservableCollection<T> AddedItems { get; }
        public ReadOnlyObservableCollection<T> RemovedItems { get; }


        //реакция на изменение коллекции (добавление или удаление элемента коллекции)
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var added = this.Except(_originalCollection).ToList();              //список добавленных элементов
            var removed = _originalCollection.Except(this).ToList();            //список удаленных элементов

            UpdateObservableCollection(added, _addedItems);
            UpdateObservableCollection(removed, _removedItems);

            base.OnCollectionChanged(e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// Синхронизация элементов коллекций.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="observableCollection"></param>
        private void UpdateObservableCollection(IList<T> items, ObservableCollection<T> observableCollection)
        {
            observableCollection.Clear();
            items.ToList().ForEach(observableCollection.Add);
        }

        /// <summary>
        /// Изменена ли коллекция?
        /// </summary>
        public bool IsChanged => _addedItems.Count > 0 || _removedItems.Count > 0;

        /// <summary>
        /// Принять все изменения в коллекции
        /// </summary>
        public void AcceptChanges()
        {
            _addedItems.Clear();
            _removedItems.Clear();

            _originalCollection = this.ToList();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// Отменить все изменения в коллекции.
        /// </summary>
        public void RejectChanges()
        {
            this.Clear();

            foreach (T item in _originalCollection)
                this.Add(item);

            _addedItems.Clear();
            _removedItems.Clear();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        public bool IsValid => true;
    }
}
