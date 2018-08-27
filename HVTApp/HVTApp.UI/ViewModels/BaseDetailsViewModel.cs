using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : 
        IDetailsViewModel<TWrapper, TEntity>, INotifyPropertyChanged, IDisposable
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IUnityContainer Container;
        protected readonly IEventAggregator EventAggregator;
        protected IWrapperDataService WrapperDataService;
        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container)
        {
            Container = container;
            WrapperDataService = Container.Resolve<IWrapperDataService>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            InitSpecialCommands();
            InitDefaultGetMethods();
            InitSpecialGetMethods();
        }

        protected virtual void InitSpecialCommands() { }
        protected virtual void InitDefaultGetMethods() { }
        protected virtual void InitSpecialGetMethods() { }

        private static TWrapper GetWrapper(TEntity entity)
        {
            return (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity);
        }

        public async Task LoadAsync(TEntity entity)
        {
            //если создаём, а не редактируем
            if (await WrapperDataService.GetRepository<TEntity>().GetByIdAsync(entity.Id) == null)
            {
                Item = (TWrapper) Activator.CreateInstance(typeof(TWrapper), entity);
                return;
            }

            //если редактируем
            Item = await WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().GetByIdAsync(entity.Id);
        }

        public async Task LoadAsync(Guid id)
        {
            Item = await WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().GetByIdAsync(id);
        }

        //protected virtual async Task LoadOtherAsync() { }

        public ICommand SaveCommand { get; }

        public TWrapper Item
        {
            get { return _item; }
            protected set
            {
                if (Equals(_item, value)) return;

                if (_item != null) _item.PropertyChanged -= ItemOnPropertyChanged;
                _item = value;
                if (_item != null) _item.PropertyChanged += ItemOnPropertyChanged;

                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Событие, бросаемое для закрытия окна
        /// </summary>
        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        protected virtual async void SaveCommand_Execute()
        {
            //добавляем сущность, если ее не существовало
            if (await WrapperDataService.GetRepository<TEntity>().GetByIdAsync(Item.Model.Id) == null)
                WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().Add(Item);

            Item.AcceptChanges();
            await WrapperDataService.SaveChangesAsync();

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);

            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged && Item.IsValid;
        }

        /// <summary>
        /// Реакция на изменение любого свойства Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected async void SelectAndSetWrapper<TModel, TWrap>(IEnumerable<TModel> entities, string propertyName, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : class, IWrapper<TModel>
        {
            //выбор сущности
            var entity = await Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);

            //поиск текущего значения
            var propertyValue = (TWrap)Item.GetType().GetProperty(propertyName).GetValue(Item);

            //замена текущего значения новым
            if (entity != null && !Equals(entity.Id, propertyValue?.Model.Id))
            {
                var wrapper = await WrapperDataService.GetWrapperRepository<TModel, TWrap>().GetByIdAsync(entity.Id);
                Item.GetType().GetProperty(propertyName).SetValue(Item, wrapper);
            }
        }

        protected async void SelectAndAddInListWrapper<TModel, TWrap>(IEnumerable<TModel> entities, IList<TWrap> list, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : WrapperBase<TModel>
        {
            var entity = await Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);
            if (entity != null)
            {
                var wrapper = await WrapperDataService.GetWrapperRepository<TModel, TWrap>().GetByIdAsync(entity.Id);
                list.Add(wrapper);
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Dispose()
        {
            WrapperDataService?.Dispose();
        }

        protected virtual void OnCloseRequested(DialogRequestCloseEventArgs e)
        {
            CloseRequested?.Invoke(this, e);
        }
    }
}