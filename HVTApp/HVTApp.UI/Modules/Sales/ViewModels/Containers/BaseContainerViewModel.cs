using System;
using System.Data.Entity.Infrastructure;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public abstract class BaseContainerViewModel<TItem, TLookup, TAfterSaveItemEvent, TAfterRemoveItemEvent, TEditView> :
        BaseContainer<TItem, TLookup, TAfterSaveItemEvent, TAfterRemoveItemEvent>
        where TItem : class, IBaseEntity
        where TLookup : LookupItem<TItem>
        where TAfterSaveItemEvent : PubSubEvent<TItem>, new()
        where TAfterRemoveItemEvent : PubSubEvent<TItem>, new()
    {
        #region Commands

        /// <summary>
        /// Команда удаления выбранной сущности
        /// </summary>
        public ICommand RemoveSelectedItemCommand { get; }

        /// <summary>
        /// Команда редактирования выбранной сущности
        /// </summary>
        public ICommand EditSelectedItemCommand { get; }

        #endregion

        protected BaseContainerViewModel(IUnityContainer container) : base(container)
        {
            RemoveSelectedItemCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                RemoveSelectedItem,
                () => this.SelectedItem != null);

            EditSelectedItemCommand = new DelegateLogCommand(this.EditSelectedItem, () => this.SelectedItem != null);

            this.SelectedItemChangedEvent += lookup =>
            {
                ((DelegateLogConfirmationCommand) this.RemoveSelectedItemCommand).RaiseCanExecuteChanged();
                ((DelegateLogCommand) this.EditSelectedItemCommand).RaiseCanExecuteChanged();
            };
        }

        public virtual void RemoveSelectedItem()
        {
            if (SelectedItem == null) throw new ArgumentNullException(nameof(SelectedItem));

            var unitOfWork = Container.Resolve<IUnitOfWork>();

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
                    Container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                }
            }
        }

        public virtual void EditSelectedItem()
        {
            var navigationParameters = new NavigationParameters { { string.Empty, SelectedItem.Entity } };
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<TEditView>(navigationParameters);
        }
    }
}