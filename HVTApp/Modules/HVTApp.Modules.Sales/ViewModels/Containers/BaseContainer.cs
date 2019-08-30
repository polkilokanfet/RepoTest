using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class BaseContainer<T, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, 
        TAfterRemoveItemEvent, TCollection> : INotifyPropertyChanged
        where T : class, IBaseEntity
        where TLookup : LookupItem<T> 
        where TSelectedItemChangedEvent : PubSubEvent<T>, new()
        where TAfterSaveItemEvent : PubSubEvent<T>, new()
        where TAfterRemoveItemEvent : PubSubEvent<T>, new()
        where TCollection : ItemsCollection<T, TLookup>
    {
        private readonly IUnityContainer _container;
        private TLookup _selectedItem;

        public TCollection Items { get; }

        /// <summary>
        /// Выбранная в настоящий момент сущность 
        /// </summary>
        public TLookup SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value))
                    return;
                _selectedItem = value;
                _container.Resolve<IEventAggregator>().GetEvent<TSelectedItemChangedEvent>().Publish(SelectedItem.Entity);
                OnPropertyChanged();
            }
        }

        protected BaseContainer(IUnityContainer container)
        {
            _container = container;

            var unitOfWork = container.Resolve<IUnitOfWork>();
            Items = GetItemsCollection(unitOfWork);

            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(AfterSaveItemEventExecute);
            eventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(AfterRemoveOfferEventExecute);
        }

        protected abstract TCollection GetItemsCollection(IUnitOfWork unitOfWork);


        /// <summary>
        /// Реакция на сохранение сущности
        /// </summary>
        /// <param name="item"> Сохраненная сущность </param>
        private void AfterSaveItemEventExecute(T item)
        {
            if (AllItems.ContainsById(item))
            {
                RefreshItem(item);
                return;
            }

            AllItems.Add(item);
        }

        private void RefreshItem(T item)
        {
            AllItems.RemoveById(item);
            AllItems.Add(item);

            //если обновлена отображаемая сущность, обновляем её
            if (this.ContainsById(item))
            {
                this.GetById(item).Refresh(item);
            }
        }

        /// <summary>
        /// Реакция на удаление ТКП
        /// </summary>
        /// <param name="item"></param>
        private void AfterRemoveOfferEventExecute(T item)
        {
            AllItems.RemoveById(item);

            if(this.ContainsById(item))
                this.RemoveById(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}