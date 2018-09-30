using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent> : 
        ViewModelBase, IDetailsViewModel<TWrapper, TEntity>, IDisposable
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
    {
        protected readonly IEventAggregator EventAggregator;
        private TWrapper _item;
        private bool _isLoaded;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        protected BaseDetailsViewModel(IUnityContainer container) : base(container)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);
            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);

            InitSpecialCommands();
            InitSpecialGetMethods();
        }

        /// <summary>
        /// Инициализация нестандартных команд.
        /// </summary>
        protected virtual void InitSpecialCommands() { }
        /// <summary>
        /// Инициализация нестандартных Get-методов.
        /// </summary>
        protected virtual void InitSpecialGetMethods() { }

        public async Task LoadAsync(TEntity entity, IUnitOfWork unitOfWork)
        {
            IsLoaded = false;
            UnitOfWork = unitOfWork;
            var item = await UnitOfWork.Repository<TEntity>().GetByIdAsync(entity.Id);
            //создаем или редактируем
            Item = item == null ? (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity)
                                : (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            await AfterLoading();
        }


        public async Task LoadAsync(TEntity entity)
        {
            IsLoaded = false;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var item = await UnitOfWork.Repository<TEntity>().GetByIdAsync(entity.Id);
            //создаем или редактируем
            Item = item == null ? (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity)
                                : (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            await AfterLoading();
        }

        public async Task LoadAsync(Guid id)
        {
            IsLoaded = false;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var item = await UnitOfWork.Repository<TEntity>().GetByIdAsync(id);
            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            await AfterLoading();
        }

        protected virtual async Task AfterLoading()
        {
            IsLoaded = true;
        }

        public void Load(TWrapper wrapper, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Item = wrapper;
            IsLoaded = true;
        }


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
        /// Событие, бросаемое для закрытия окна
        /// </summary>
        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        protected virtual void OkCommand_Execute()
        {
            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool OkCommand_CanExecute()
        {
            return Item != null && Item.IsValid;
        }

        protected virtual async void SaveCommand_Execute()
        {
            //добавляем сущность, если ее не существовало
            if (await UnitOfWork.Repository<TEntity>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.Repository<TEntity>().Add(Item.Model);

            Item.AcceptChanges();
            //сохраняем
            try
            {
                await UnitOfWork.SaveChangesAsync();
                EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var sb = new StringBuilder();
                Exception exception = e;
                do
                {
                    sb.AppendLine(e.Message);
                    exception = exception.InnerException;
                } while (exception != null);

                Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", sb.ToString());
            }

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
            ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //если были какие-то изменения
            if (SaveCommand_CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить изменения?") == MessageDialogResult.Yes)
                {
                    SaveCommand_Execute();
                }
            }


            base.GoBackCommand_Execute();
        }

        protected async void SelectAndSetWrapper<TModel, TWrap>(IEnumerable<TModel> entities, string propertyName, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : class, IWrapper<TModel>
        {
            //выбор сущности
            var entity = Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);

            //поиск текущего значения
            var propertyValue = (TWrap)Item.GetType().GetProperty(propertyName).GetValue(Item);

            //замена текущего значения новым
            if (entity != null && !Equals(entity.Id, propertyValue?.Model.Id))
            {
                var item = await UnitOfWork.Repository<TModel>().GetByIdAsync(entity.Id);
                var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
                Item.GetType().GetProperty(propertyName).SetValue(Item, wrapper);
            }
        }

        protected async void SelectAndAddInListWrapper<TModel, TWrap>(IEnumerable<TModel> entities, IList<TWrap> list, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : WrapperBase<TModel>
        {
            var entity = Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);
            if (entity != null)
            {
                var item = await UnitOfWork.Repository<TModel>().GetByIdAsync(entity.Id);
                var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
                list.Add(wrapper);
            }
        }


        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        protected virtual void OnCloseRequested(DialogRequestCloseEventArgs e)
        {
            CloseRequested?.Invoke(this, e);
        }
    }
}