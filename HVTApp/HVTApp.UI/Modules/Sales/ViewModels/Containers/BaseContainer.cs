using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public abstract class BaseContainer<TItem, TLookup, TSelectedItemChangedEvent, TAfterSaveItemEvent, TAfterRemoveItemEvent> : 
        ObservableCollection<TLookup>
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
        /// Выбранная в настоящий момент сущность 
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

            var unitOfWork = container.Resolve<IUnitOfWork>();
            AllLookups = GetLookups(unitOfWork).ToList();

            var eventAggregator = container.Resolve<IEventAggregator>();

            //реакция на сохранение сущности
            eventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(OnAfterSaveItemEvent);
            
            //реакция на удаление сущности
            eventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(OnAfterRemoveItemEvent);

            //отслеживание актуальности выбранного юнита
            this.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (TLookup lookup in args.OldItems)
                    {
                        //актуализируем выбранный юнит
                        if (SelectedItem != null && SelectedItem.Id == lookup.Id)
                            SelectedItem = null;
                    }
                }
            };
        }

        /// <summary>
        /// Реакция на удаление сущности
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnAfterRemoveItemEvent(TItem item)
        {
            //удаляем сущность из списка всех сущностей
            AllLookups.RemoveIfContainsById(item);

            //удаление сущности из отображаемой части
            this.RemoveIfContainsById(item);
        }

        /// <summary>
        /// Реакция на сохранение сущности
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnAfterSaveItemEvent(TItem item)
        {
            //если сущность была изменена
            if (AllLookups.ContainsById(item))
            {
                //обновляем отображение сущности
                AllLookups.GetById(item).Refresh(item);
                return;
            }

            //если сущность была вновь создана
            //добавляем в список всех сущностей
            var newLookup = MakeLookup(item);
            AllLookups.Add(newLookup);

            //добавляем в список отображения
            if (CanBeShown(item))
            {
                this.Add(newLookup);
            }
        }

        /// <summary>
        /// Создание отображения сущности.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected  virtual TLookup MakeLookup(TItem item)
        {
            return (TLookup) Activator.CreateInstance(typeof(TLookup), item);
        }

        /// <summary>
        /// Сущность должна быть показана при текущих фильтрах
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool CanBeShown(TItem item)
        {
            return true;
        }

        protected abstract IEnumerable<TLookup> GetLookups(IUnitOfWork unitOfWork);

        public virtual void RemoveSelectedItem()
        {
            if(SelectedItem == null) throw new ArgumentNullException(nameof(SelectedItem));

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var messageService = Container.Resolve<IMessageService>();

            var dr = messageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedItem.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;

            var entity = unitOfWork.Repository<TItem>().GetById(SelectedItem.Id);
            if (entity != null)
            {
                unitOfWork.Repository<TItem>().Delete(entity);
                try
                {
                    unitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<TAfterRemoveItemEvent>().Publish(entity);
                }
                catch (DbUpdateException e)
                {
                    messageService.ShowOkMessageDialog(e.GetType().ToString(), e.GetAllExceptions());
                }
            }

        }
    }
}