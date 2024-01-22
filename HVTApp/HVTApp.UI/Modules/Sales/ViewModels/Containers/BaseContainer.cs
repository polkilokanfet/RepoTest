using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public abstract class BaseContainer<TItem, TLookup, TAfterSaveItemEvent, TAfterRemoveItemEvent> : 
        ObservableCollection<TLookup>, IDisposable
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem> 
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IEventAggregator EventAggregator;
        protected List<TLookup> AllLookups = new List<TLookup>();

        public event Action<TLookup> SelectedItemChangedEvent;

        private TLookup _selectedItem;
        /// <summary>
        /// ��������� � ��������� ������ �������� 
        /// </summary>
        public TLookup SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
                SelectedItemChangedEvent?.Invoke(value);
            }
        }

        protected BaseContainer(IUnityContainer container)
        {
            Container = container;
            EventAggregator = container.Resolve<IEventAggregator>();

            //������� �� ���������� ��������
            EventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(OnAfterSaveItemEvent);
            //������� �� �������� ��������
            EventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(OnAfterRemoveItemEvent);

            //������������ ������������ ���������� �����
            this.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (TLookup lookup in args.OldItems)
                    {
                        //������������� ��������� ����
                        if (SelectedItem != null && SelectedItem.Id == lookup.Id)
                            SelectedItem = null;
                    }
                }
            };
        }

        public void Load(IUnitOfWork unitOfWork)
        {
            AllLookups = GetLookups(unitOfWork).ToList();
        }

        /// <summary>
        /// ������� �� �������� ��������
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnAfterRemoveItemEvent(TItem item)
        {
            //������� �������� �� ������ ���� ���������
            AllLookups.RemoveIfContainsById(item);

            //�������� �������� �� ������������ �����
            this.RemoveIfContainsById(item);
        }

        /// <summary>
        /// ������� �� ���������� ��������
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnAfterSaveItemEvent(TItem item)
        {
            //���� �������� ���� ��������
            if (AllLookups.ContainsById(item))
            {
                //��������� ����������� ��������
                AllLookups.GetById(item).Refresh(item);
                return;
            }

            //���� �������� ���� ����� �������
            //��������� � ������ ���� ���������
            var newLookup = MakeLookup(item);
            AllLookups.Add(newLookup);

            //��������� � ������ �����������
            if (CanBeShown(item))
            {
                this.Add(newLookup);
            }
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
        /// <param name="calculation"></param>
        /// <returns></returns>
        protected virtual bool CanBeShown(TItem calculation)
        {
            return true;
        }

        protected abstract IEnumerable<TLookup> GetLookups(IUnitOfWork unitOfWork);

        public void Dispose()
        {
            EventAggregator.GetEvent<TAfterSaveItemEvent>().Unsubscribe(OnAfterSaveItemEvent);
            EventAggregator.GetEvent<TAfterRemoveItemEvent>().Unsubscribe(OnAfterRemoveItemEvent);
        }
    }
}