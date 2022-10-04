using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.Wrapper.Base.TrackingCollections
{
    /// <summary>
    /// Коллекция способная отслеживать изменилось ли в ней что-то.
    /// </summary>
    /// <typeparam name="TCollectionItem"></typeparam>
    public class ValidatableChangeTrackingCollection<TCollectionItem> : ObservableCollection<TCollectionItem>, IValidatableChangeTrackingCollection<TCollectionItem> 
        where TCollectionItem: class, IValidatableChangeTracking
    {
        /// <summary>
        /// Начальный (оригинальный) список членов коллекции.
        /// </summary>
        private IList<TCollectionItem> _originalCollection; 

        private readonly ObservableCollection<TCollectionItem> _addedItems = new ObservableCollection<TCollectionItem>();
        private readonly ObservableCollection<TCollectionItem> _modifiedItems = new ObservableCollection<TCollectionItem>();
        private readonly ObservableCollection<TCollectionItem> _removedItems = new ObservableCollection<TCollectionItem>();

        public ValidatableChangeTrackingCollection(IEnumerable<TCollectionItem> items) : base(items)
        {
            //фиксируем то, что мы имели изначально.
            _originalCollection = this.ToList();

            AttachedItemPropertyChangedHandler(_originalCollection);

            AddedItems = new ReadOnlyObservableCollection<TCollectionItem>(_addedItems);
            ModifiedItems = new ReadOnlyObservableCollection<TCollectionItem>(_modifiedItems);
            RemovedItems = new ReadOnlyObservableCollection<TCollectionItem>(_removedItems);
        }

        public ReadOnlyObservableCollection<TCollectionItem> AddedItems { get; }
        public ReadOnlyObservableCollection<TCollectionItem> ModifiedItems { get; }
        public ReadOnlyObservableCollection<TCollectionItem> RemovedItems { get; }

        /// <summary>
        /// Изменена ли коллекция?
        /// </summary>
        public bool IsChanged => _addedItems.Count > 0 || _modifiedItems.Count > 0 || _removedItems.Count > 0;

        /// <summary>
        /// Валидны ли все члены коллекции?
        /// </summary>
        public virtual bool IsValid => this.All(x => x.IsValid);

        /// <summary>
        /// Принять все изменения в коллекции
        /// </summary>
        public void AcceptChanges()
        {
            _addedItems.Clear();
            _modifiedItems.Clear();
            _removedItems.Clear();

            this.ToList().ForEach(x => x.AcceptChanges());

            _originalCollection = this.ToList();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// Отменить все изменения в коллекции.
        /// </summary>
        public void RejectChanges()
        {
            //отмена изменений в оригинальной коллекции
            _originalCollection.ForEach(x =>
            {
                if (x.IsChanged)
                {
                    x.RejectChanges();
                }
            });

            //добавление удаленных членов коллекции
            var removedItems = _removedItems.ToList();
            this.AddRange(removedItems);

            //удаление добавленных членов коллекции
            var addedItems = _addedItems.ToList();
            addedItems.ForEach(x => this.Remove(x));

            //очистка коллекций
            _addedItems.Clear();
            _modifiedItems.Clear();
            _removedItems.Clear();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        /// <summary>
        /// реакция на изменение коллекции (добавление или удаление элемента коллекции)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            //отписываем все члены коллекции от события изменения
            var dettachedItems = this.Concat(_originalCollection).Concat(_addedItems).ToList();
            DettachedItemPropertyChangedHandler(dettachedItems);

            //подписываем члены коллекции к событию
            AttachedItemPropertyChangedHandler(this);

            var added = this.Except(_originalCollection).ToList();               //список добавленных элементов
            var removed = _originalCollection.Except(this).ToList();             //список удаленных элементов
            var modified = this.Except(added).Where(x => x.IsChanged).ToList();  //список измененных элементов

            UpdateObservableCollection(added, _addedItems);
            UpdateObservableCollection(removed, _removedItems);
            UpdateObservableCollection(modified, _modifiedItems);

            base.OnCollectionChanged(e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
        }

        /// <summary>
        /// Обработчик изменения какого-либо свойства в члене коллекции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //если изменился флаг валидности члена.
            if (e.PropertyName == nameof(IsValid)) OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));

            if (e.PropertyName == nameof(IsChanged))
            {
                //объект в котором изменилось свойство.
                var item = (TCollectionItem) sender;

                //если этот объект добавлен в этом сеансе, нет смысла реагировать на изменение его свойств.
                if (!_addedItems.Contains(item))
                {
                    //если изменился объект (флаг IsChanged об этом говорит).
                    if (item.IsChanged)
                    {
                        //добавляем член в коллекцию измененных объектов, если он еще не в этой коллекции.
                        if (!_modifiedItems.Contains(item)) _modifiedItems.Add(item);
                    }
                    else
                    {
                        //если объект не изменился, удяляем его из коллекции измененных объектов (если он там есть).
                        if (_modifiedItems.Contains(item)) _modifiedItems.Remove(item);
                    }

                    //информируем о том, что коллекция изменилась.
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
                }
            }
        }

        /// <summary>
        /// прикрепление обработчика к событию изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void AttachedItemPropertyChangedHandler(IEnumerable<TCollectionItem> items)
        {
            items.ToList().ForEach(x => x.PropertyChanged += OnItemPropertyChanged);
        }

        /// <summary>
        /// открепление обработчика от события изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void DettachedItemPropertyChangedHandler(IEnumerable<TCollectionItem> items)
        {
            items.ToList().ForEach(x => x.PropertyChanged -= OnItemPropertyChanged);
        }

        /// <summary>
        /// Синхронизация элементов коллекций.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="observableCollection"></param>
        private void UpdateObservableCollection(IEnumerable<TCollectionItem> items, ICollection<TCollectionItem> observableCollection)
        {
            observableCollection.Clear();
            items.ToList().ForEach(observableCollection.Add);
        }
    }
}
