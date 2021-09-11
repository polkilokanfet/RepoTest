using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;
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
            get => _isLoaded;
            set
            {
                _isLoaded = value;
                RaisePropertyChanged();
            }
        }

        protected BaseDetailsViewModel(IUnityContainer container) : base(container)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            EventAggregator = Container.Resolve<IEventAggregator>();

            SaveCommand = new DelegateLogCommand(SaveItem, SaveCommand_CanExecute);
            OkCommand = new DelegateLogCommand(OkCommand_Execute, OkCommand_CanExecute);

            InitSpecialCommands();
            InitSpecialGetMethods();
        }

        /// <summary>
        /// »нициализаци€ нестандартных команд.
        /// </summary>
        protected virtual void InitSpecialCommands() { }
        /// <summary>
        /// »нициализаци€ нестандартных Get-методов.
        /// </summary>
        protected virtual void InitSpecialGetMethods() { }

        public void Load(TEntity entity, IUnitOfWork unitOfWork)
        {
            IsLoaded = false;
            UnitOfWork = unitOfWork;
            var item = UnitOfWork.Repository<TEntity>().GetById(entity.Id);
            //создаем или редактируем
            Item = item == null ? (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity)
                                : (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            AfterLoading();
        }

        public void Load(TEntity entity)
        {
            IsLoaded = false;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var item = UnitOfWork.Repository<TEntity>().GetById(entity.Id);
            //создаем или редактируем
            Item = item == null ? (TWrapper)Activator.CreateInstance(typeof(TWrapper), entity)
                                : (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            AfterLoading();
        }

        public void Load(Guid id)
        {
            IsLoaded = false;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var item = UnitOfWork.Repository<TEntity>().GetById(id);
            Item = (TWrapper)Activator.CreateInstance(typeof(TWrapper), item);
            AfterLoading();
        }

        public void Load(TWrapper wrapper, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Item = wrapper;
            IsLoaded = true;
        }

        protected virtual void AfterLoading()
        {
            IsLoaded = true;
        }


        public ICommand SaveCommand { get; }
        public ICommand OkCommand { get; }

        public TEntity Entity => Item.Model;

        public TWrapper Item
        {
            get => _item;
            protected set
            {
                if (Equals(_item, value)) return;

                if (_item != null) _item.PropertyChanged -= ItemOnPropertyChanged;
                _item = value;
                if (_item != null) _item.PropertyChanged += ItemOnPropertyChanged;

                ((DelegateLogCommand)SaveCommand).RaiseCanExecuteChanged();
                ((DelegateLogCommand)OkCommand).RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// —обытие, бросаемое дл€ закрыти€ окна
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

        /// <summary>
        /// ѕроверка возможности сохранить
        /// </summary>
        /// <returns></returns>
        protected virtual bool AllowSave()
        {
            return true;
        }

        protected virtual void SaveItem()
        {
            if (AllowSave() == false) return;

            //добавл€ем сущность, если ее не существовало
            if (UnitOfWork.Repository<TEntity>().GetById(Item.Model.Id) == null)
                UnitOfWork.Repository<TEntity>().Add(Item.Model);

            Item.AcceptChanges();
            //сохран€ем
            try
            {
                UnitOfWork.SaveChanges();
                EventAggregator.GetEvent<TAfterSaveEntityEvent>().Publish(Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                //Container.Resolve<IMessageService>().ShowOkMessageDialog(saveTask.Exception?.GetType().ToString(), saveTask.Exception.PrintAllExceptions());
                Container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
            }

            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected virtual bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged && Item.IsValid;
        }

        /// <summary>
        /// –еакци€ на изменение любого свойства Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateLogCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateLogCommand)OkCommand).RaiseCanExecuteChanged();
        }

        protected override void GoBackCommand_Execute()
        {
            //если были какие-то изменени€
            if (SaveCommand_CanExecute())
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("—охранение", "—охранить изменени€?", defaultNo:true) == MessageDialogResult.Yes)
                {
                    SaveItem();
                }
            }

            base.GoBackCommand_Execute();
        }

        protected void SelectAndSetWrapper<TModel, TWrap>(IEnumerable<TModel> entities, string propertyName, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : class, IWrapper<TModel>
        {
            //выбор сущности
            var entity = Container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);

            //поиск текущего значени€
            var propertyValue = (TWrap)Item.GetType().GetProperty(propertyName).GetValue(Item);

            //замена текущего значени€ новым
            if (entity != null && !Equals(entity.Id, propertyValue?.Model.Id))
            {
                var item = UnitOfWork.Repository<TModel>().GetById(entity.Id);
                var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
                Item.GetType().GetProperty(propertyName).SetValue(Item, wrapper);
            }
        }

        protected void SelectAndAddInListWrapper<TModel, TWrap>(IEnumerable<TModel> entities, IList<TWrap> list)
            where TModel : class, IBaseEntity
            where TWrap : WrapperBase<TModel>
        {
            //вычищение того, что уже есть в списке
            List<TModel> targetEntities = entities.ToList();
            list.Select(wrap => wrap.Model).ForEach(model => targetEntities.RemoveIfContainsById(model));

            var selectItems = Container.Resolve<ISelectService>().SelectItems(targetEntities);
            if (selectItems != null)
            {
                foreach (var selectItem in selectItems)
                {
                    var item = UnitOfWork.Repository<TModel>().GetById(selectItem.Id);
                    var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
                    list.Add(wrapper);
                }
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