using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HVTApp.Modules.Sales.ViewModels
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

            var unitOfWork = container.Resolve<IUnitOfWorkDisplay>();
            AllLookups = GetLookups(unitOfWork).ToList();

            var eventAggregator = container.Resolve<IEventAggregator>();

            //реакция на сохранение сущности
            eventAggregator.GetEvent<TAfterSaveItemEvent>().Subscribe(item =>
            {
                //если сущность была изменена
                if (AllLookups.ContainsById(item))
                {
                    AllLookups.GetById(item).Refresh(item);
                    return;
                }

                //если сущность была вновь создана
                //добавляем в список всех сущностей
                var newLookup = MakeLookup(item);
                AllLookups.Add(newLookup);

                //добавляем в список отображения
                if (CanBeShown(newLookup))
                {
                    this.Add(newLookup);
                }
            });
            
            //реакция на удаление сущности
            eventAggregator.GetEvent<TAfterRemoveItemEvent>().Subscribe(item =>
            {
                //удаляем сущность из списка всех сущностей
                AllLookups.RemoveIfContainsById(item);

                //актуализируем выбранный юнит
                if (SelectedItem != null && SelectedItem.Id == item.Id)
                    SelectedItem = null;

                //удаление сущности из отображаемой части
                this.RemoveIfContainsById(item);
            });

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
        /// <param name="lookup"></param>
        /// <returns></returns>
        protected virtual bool CanBeShown(TLookup lookup)
        {
            return true;
        }

        protected abstract IEnumerable<TLookup> GetLookups(IUnitOfWorkDisplay unitOfWork);

        public virtual async Task RemoveSelectedItemTask()
        {
            if(SelectedItem == null) throw new ArgumentNullException(nameof(SelectedItem));

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var messageService = Container.Resolve<IMessageService>();

            var dr = messageService.ShowYesNoMessageDialog("Удаление", 
                $"Вы действительно хотите удалить \"{SelectedItem.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;


            var entity = await unitOfWork.Repository<TItem>().GetByIdAsync(SelectedItem.Id);
            if (entity != null)
            {
                unitOfWork.Repository<TItem>().Delete(entity);
                try
                {
                    await unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    messageService.ShowOkMessageDialog(e.GetType().ToString(), e.GetAllExceptions());
                }
            }

            Container.Resolve<IEventAggregator>().GetEvent<TAfterRemoveItemEvent>().Publish(entity);
        }

    }
}