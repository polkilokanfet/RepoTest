using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : 
        ViewModelBase, IDetailsViewModel<TWrapper, TEntity>, IDisposable
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IEventAggregator EventAggregator;
        protected IWrapperDataService WrapperDataService;
        private TWrapper _item;

        protected BaseDetailsViewModel(IUnityContainer container) : base(container)
        {
            WrapperDataService = Container.Resolve<IWrapperDataService>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);

            InitSpecialCommands();
            InitSpecialGetMethods();
        }

        /// <summary>
        /// ������������� ������������� ������.
        /// </summary>
        protected virtual void InitSpecialCommands() { }
        /// <summary>
        /// ������������� ������������� Get-�������.
        /// </summary>
        protected virtual void InitSpecialGetMethods() { }

        public async Task LoadAsync(TEntity entity)
        {
            WrapperDataService = Container.Resolve<IWrapperDataService>();
            var item = await WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().GetByIdAsync(entity.Id);
            //������� ��� �����������
            Item = item ?? (TWrapper) Activator.CreateInstance(typeof(TWrapper), entity);
            await AfterLoading();
        }

        public async Task LoadAsync(Guid id)
        {
            WrapperDataService?.Dispose();
            WrapperDataService = Container.Resolve<IWrapperDataService>();
            Item = await WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().GetByIdAsync(id);
            await AfterLoading();
        }

        protected virtual async Task AfterLoading() { }

        public ICommand SaveCommand { get; }
        public ICommand OkCommand { get; }

        public TEntity Entity => Item.Model;

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
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// �������, ��������� ��� �������� ����
        /// </summary>
        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        protected virtual void OkCommand_Execute()
        {
            //����������� �������� ����
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool OkCommand_CanExecute()
        {
            return Item != null && Item.IsValid;
        }

        protected virtual async void SaveCommand_Execute()
        {
            //��������� ��������, ���� �� �� ������������
            if (await WrapperDataService.Repository<TEntity>().GetByIdAsync(Item.Model.Id) == null)
                WrapperDataService.GetWrapperRepository<TEntity, TWrapper>().Add(Item);

            Item.AcceptChanges();
            await WrapperDataService.SaveChangesAsync();

            EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);

            //����������� �������� ����
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged && Item.IsValid;
        }

        /// <summary>
        /// ������� �� ��������� ������ �������� Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        protected async void SelectAndSetWrapper<TModel, TWrap>(IEnumerable<TModel> entities, string propertyName, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : class, IWrapper<TModel>
        {
            //����� ��������
            var entity = await Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);

            //����� �������� ��������
            var propertyValue = (TWrap)Item.GetType().GetProperty(propertyName).GetValue(Item);

            //������ �������� �������� �����
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