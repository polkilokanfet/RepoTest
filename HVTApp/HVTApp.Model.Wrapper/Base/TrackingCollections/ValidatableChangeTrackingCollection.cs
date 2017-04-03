using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    /// <summary>
    /// Коллекция способная отслеживать изменилось ли в ней что-то.
    /// </summary>
    /// <typeparam name="TCollectionItem"></typeparam>
    public class ValidatableChangeTrackingCollection<TCollectionItem> : ObservableCollection<TCollectionItem>, IValidatableChangeTracking
        where TCollectionItem: class, IValidatableChangeTracking
    {
        /// <summary>
        /// Начальный (оригинальный) список членов коллекции.
        /// </summary>
        private IList<TCollectionItem> _originalCollection; 

        private readonly ObservableCollection<TCollectionItem> _addedItems;
        private readonly ObservableCollection<TCollectionItem> _modifiedItems;
        private readonly ObservableCollection<TCollectionItem> _removedItems;

        public ValidatableChangeTrackingCollection(IEnumerable<TCollectionItem> items) : base(items)
        {
            //фиксируем то, что мы имели изначально.
            _originalCollection = this.ToList();

            AttachedItemPropertyChangedHandler(_originalCollection);

            _addedItems = new ObservableCollection<TCollectionItem>();
            _modifiedItems = new ObservableCollection<TCollectionItem>();
            _removedItems = new ObservableCollection<TCollectionItem>();

            AddedItems = new ReadOnlyObservableCollection<TCollectionItem>(_addedItems);
            ModifiedItems = new ReadOnlyObservableCollection<TCollectionItem>(_modifiedItems);
            RemovedItems = new ReadOnlyObservableCollection<TCollectionItem>(_removedItems);
        }

        public ReadOnlyObservableCollection<TCollectionItem> AddedItems { get; }
        public ReadOnlyObservableCollection<TCollectionItem> ModifiedItems { get; }
        public ReadOnlyObservableCollection<TCollectionItem> RemovedItems { get; }

        /// <summary>
        /// прикрепление обработчика к событию изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void AttachedItemPropertyChangedHandler(IList<TCollectionItem> items)
        {
            items.ToList().ForEach(x => x.PropertyChanged += OnItemPropertyChanged);
        }

        /// <summary>
        /// открепление обработчика от события изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void DettachedItemPropertyChangedHandler(IList<TCollectionItem> items)
        {
            items.ToList().ForEach(x => x.PropertyChanged -= OnItemPropertyChanged);
        }

        /// <summary>
        /// Обработчик изменения какого-либо свойства в члене коллекции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //если изменился флаг валидности члена.
            if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(sender, nameof(IsValid));
            }
            else
            {
                //объект в котором изменилось свойство.
                var item = (TCollectionItem) sender;
                //если этот объект добавлен в этом сеансе, нет смысла реагировать на изменение его свойств.
                if (_addedItems.Contains(item))
                {
                    //информируем о том, что коллекция изменилась.
                    OnPropertyChanged(sender, nameof(IsChanged));
                    return;
                }

                //если изменился объект (флаг IsChanged об этом говорит).
                if (item.IsChanged)
                {
                    //добавляем член в коллекцию измененных объектов, если он еще не в этой коллекции.
                    if (!_modifiedItems.Contains(item))
                    {
                        _modifiedItems.Add(item);
                    }
                }
                else
                {
                    //если объект не изменился, удяляем его из коллекции измененных объектов (если он там есть).
                    if (_modifiedItems.Contains(item))
                    {
                        _modifiedItems.Remove(item);
                    }
                }

                //информируем о том, что коллекция изменилась.
                OnPropertyChanged(sender, nameof(IsChanged));
            }

        }

        //реакция на изменение коллекции (добавление или удаление элемента коллекции)
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var added = this.Except(_originalCollection).ToList();              //список добавленных элементов
            var removed = _originalCollection.Except(this).ToList();            //список удаленных элементов
            var changed = this.Except(added).Where(x => x.IsChanged).ToList();  //список измененных элементов

            var dettachedItems = _addedItems.Concat(_modifiedItems).Concat(_removedItems).Concat(added).Concat(removed).ToList();
            DettachedItemPropertyChangedHandler(dettachedItems);

            AttachedItemPropertyChangedHandler(added);

            UpdateObservableCollection(added, _addedItems);
            UpdateObservableCollection(removed, _removedItems);
            UpdateObservableCollection(changed, _modifiedItems);

            base.OnCollectionChanged(e);
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
        }

        /// <summary>
        /// Синхронизация элементов коллекций.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="observableCollection"></param>
        private void UpdateObservableCollection(IList<TCollectionItem> items, ObservableCollection<TCollectionItem> observableCollection)
        {
            observableCollection.Clear();
            items.ToList().ForEach(observableCollection.Add);
        }

        /// <summary>
        /// Изменена ли коллекция?
        /// </summary>
        public bool IsChanged => _addedItems.Count > 0 || _modifiedItems.Count > 0 || _removedItems.Count > 0;

        /// <summary>
        /// Валидны ли все члены коллекции?
        /// </summary>
        public bool IsValid => this.All(x => x.IsValid);

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
            this.Clear();

            foreach (TCollectionItem item in _originalCollection)
            {
                if (item.IsChanged)
                    item.RejectChanges();
                this.Add(item);
            }

            _addedItems.Clear();
            _modifiedItems.Clear();
            _removedItems.Clear();

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
        }

        public bool IsBusy => false;

        private readonly List<WhoRised> _whoRisedEventPropertyChanged = new List<WhoRised>();

        protected void OnPropertyChanged(object sender, string propertyName)
        {
            WhoRised whoRised = new WhoRised(sender, propertyName);
            if (!_whoRisedEventPropertyChanged.Contains(whoRised))
            {
                _whoRisedEventPropertyChanged.Add(whoRised);
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                _whoRisedEventPropertyChanged.Remove(whoRised);
            }
        }

    }
}
