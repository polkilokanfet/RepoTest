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
    /// <typeparam name="T"></typeparam>
    public class ValidatableChangeTrackingCollection<T> : ObservableCollection<T>, IValidatableChangeTracking
        where T: class, IValidatableChangeTracking
    {
        /// <summary>
        /// Начальный (оригинальный) список членов коллекции.
        /// </summary>
        private IList<T> _originalCollection; 

        private readonly ObservableCollection<T> _addedItems;
        private readonly ObservableCollection<T> _modifiedItems;
        private readonly ObservableCollection<T> _removedItems;

        public ValidatableChangeTrackingCollection(IEnumerable<T> items) : base(items)
        {
            //фиксируем то, что мы имели изначально.
            _originalCollection = this.ToList();

            AttachedItemPropertyChangedHendler(_originalCollection);

            _addedItems = new ObservableCollection<T>();
            _modifiedItems = new ObservableCollection<T>();
            _removedItems = new ObservableCollection<T>();

            AddedItems = new ReadOnlyObservableCollection<T>(_addedItems);
            ModifiedItems = new ReadOnlyObservableCollection<T>(_modifiedItems);
            RemovedItems = new ReadOnlyObservableCollection<T>(_removedItems);
        }

        public ReadOnlyObservableCollection<T> AddedItems { get; }
        public ReadOnlyObservableCollection<T> ModifiedItems { get; }
        public ReadOnlyObservableCollection<T> RemovedItems { get; }

        /// <summary>
        /// прикрепление обработчика к событию изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void AttachedItemPropertyChangedHendler(IList<T> items)
        {
            items.ToList().ForEach(x => x.PropertyChanged += OnItemPropertyChanged);
        }

        /// <summary>
        /// открепление обработчика от события изменения свойств члена коллекции.
        /// </summary>
        /// <param name="items"></param>
        private void DettachedItemPropertyChangedHendler(IList<T> items)
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
                //информируем о том, что коллекция изменила свою валидность.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsValid)));
            }
            else
            {
                //объект в котором изменилось свойство.
                var item = (T) sender;
                //если этот объект добавлен в этом сеансе, нет смысла реагировать на изменение его свойств.
                if (_addedItems.Contains(item))
                {
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
            }
        }

        //реакция на изменение коллекции (добавление или удаление элемента коллекции)
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var added = this.Except(_originalCollection).ToList();              //список добавленных элементов
            var removed = _originalCollection.Except(this).ToList();            //список удаленных элементов
            var changed = this.Except(added).Where(x => x.IsChanged).ToList();  //список измененных элементов

            AttachedItemPropertyChangedHendler(added);
            DettachedItemPropertyChangedHendler(removed);

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
        private void UpdateObservableCollection(IList<T> items, ObservableCollection<T> observableCollection)
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

            foreach (T item in _originalCollection)
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
    }
}
