using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract class BaseContainer<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent> : ObservableCollection<TLookup>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem> 
        where TSelectedItemChangedEvent : PubSubEvent<TItem>, new()
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
    {
        protected readonly IUnityContainer Container;
        protected List<TItem> AllItems;

        private TLookup _selectedItem;
        /// <summary>
        /// ��������� � ��������� ������ �������� 
        /// </summary>
        public TLookup SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value))
                    return;
                _selectedItem = value;
                Container.Resolve<IEventAggregator>().GetEvent<TSelectedItemChangedEvent>().Publish(SelectedItem.Entity);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        protected BaseContainer(IUnityContainer container)
        {
            Container = container;

            var unitOfWork = container.Resolve<IUnitOfWork>();
            AllItems = GetItems(unitOfWork).ToList();

            var eventAggregator = container.Resolve<IEventAggregator>();
            // ������� �� ���������� ��������
            eventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(item => AllItems.ReAddById(item));
            
            // ������� �� �������� ��������
            eventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(item =>
            {
                AllItems.RemoveIfContainsById(item);

                if (SelectedItem != null && SelectedItem.Id == item.Id)
                    SelectedItem = null;

                this.RemoveIfContainsById(item); //�������� �������� �� ������������ �����
            });
        }

        protected abstract IEnumerable<TItem> GetItems(IUnitOfWork unitOfWork);

    }
}