using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        protected List<TLookup> AllLookups;

        private TLookup _selectedItem;
        /// <summary>
        /// ��������� � ��������� ������ �������� 
        /// </summary>
        public TLookup SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                Container.Resolve<IEventAggregator>().GetEvent<TSelectedItemChangedEvent>().Publish(SelectedItem?.Entity);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        protected BaseContainer(IUnityContainer container)
        {
            Container = container;

            var unitOfWork = container.Resolve<IUnitOfWorkDisplay>();
            AllLookups = GetLookups(unitOfWork).ToList();

            var eventAggregator = container.Resolve<IEventAggregator>();

            //������� �� ���������� ��������
            eventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(item =>
            {
                //���� �������� ���� ��������
                if (AllLookups.ContainsById(item))
                {
                    AllLookups.GetById(item).Refresh(item);
                    return;
                }

                //���� �������� ���� ����� �������
                //��������� � ������ ���� ���������
                var newLookup = MakeLookup(item);
                AllLookups.Add(newLookup);

                //��������� � ������ �����������
                if (CanBeShown(newLookup))
                {
                    this.Add(newLookup);
                }
            });
            
            //������� �� �������� ��������
            eventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(item =>
            {
                //������� �������� �� ������ ���� ���������
                AllLookups.RemoveIfContainsById(item);

                //������������� ��������� ����
                if (SelectedItem != null && SelectedItem.Id == item.Id)
                    SelectedItem = null;

                //�������� �������� �� ������������ �����
                this.RemoveIfContainsById(item);
            });

        }

        /// <summary>
        /// �������� ����������� ��������.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected  virtual TLookup MakeLookup(TItem item)
        {
            return (TLookup) Activator.CreateInstance(typeof(TLookup), item);
        }

        /// <summary>
        /// �������� ������ ���� �������� ��� ������� ��������
        /// </summary>
        /// <param name="lookup"></param>
        /// <returns></returns>
        protected virtual bool CanBeShown(TLookup lookup)
        {
            return true;
        }

        protected abstract IEnumerable<TLookup> GetLookups(IUnitOfWorkDisplay unitOfWork);
    }
}