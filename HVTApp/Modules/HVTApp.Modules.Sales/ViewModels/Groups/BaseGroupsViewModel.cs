using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public abstract partial class BaseGroupsViewModel<TGroup, TMember, TModel, TAfterSaveEvent, TAfterRemoveEvent> : ViewModelBase
        where TModel : class, IUnit
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TAfterRemoveEvent : PubSubEvent<TModel>, new()
    {

        private ProductIncludedWrapper _selectedProductIncluded;

        public GroupsCollection<TModel, TGroup, TMember> Groups { get; protected set; } = new GroupsCollection<TModel, TGroup, TMember>();

        /// <summary>
        /// Выбранный зависимый продукт.
        /// </summary>
        public ProductIncludedWrapper SelectedProductIncluded
        {
            get { return _selectedProductIncluded; }
            set
            {
                if (Equals(_selectedProductIncluded, value)) return;
                _selectedProductIncluded = value;
                ((DelegateCommand)RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        protected abstract List<TGroup> GetGroups(IEnumerable<TModel> units);

        protected void Load(IEnumerable<TModel> units, IUnitOfWork unitOfWork, bool isNew)
        {
            UnitOfWork = unitOfWork;
            var unitsArray = units as TModel[] ?? units.ToArray();

            //актуализируем количество родительских групп включенных продуктов
            foreach (var includedProduct in unitsArray.SelectMany(x => x.ProductsIncluded))
            {
                includedProduct.ParentsCount = unitsArray.Count(x => x.ProductsIncluded.Contains(includedProduct));
            }

            //группируем юниты
            var groups = GetGroups(unitsArray);

            //если создана новая сущность, юниты добавляем в пустой список
            if (isNew)
            {
                //новый контейнер групп
                Groups = new GroupsCollection<TModel, TGroup, TMember>();
                //добавление групп в контейнер
                groups.ForEach(x => Groups.Add(x));
            }
            //если сущность редактируется
            else
            {
                Groups = new GroupsCollection<TModel, TGroup, TMember>(groups);
            }

            Groups.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)RemoveCommand)?.RaiseCanExecuteChanged();
                ((DelegateCommand)AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(PriceStructures));
            };

            OnPropertyChanged(nameof(Groups));

            ((IValidatableChangeTrackingCollection<TGroup>)Groups).PropertyChanged += (sender, args) => GroupChanged?.Invoke();
            Groups.CollectionChanged += (sender, args) => GroupChanged?.Invoke();

            Groups.ForEach(RefreshPrice);
        }

        protected BaseGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => Groups.SelectedGroup != null);

            ChangeFacilityCommand = new DelegateCommand<TGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<TGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<TGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => Groups.SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);

            //загружаем блоки оборудования (если они еще не загружены)
            if(Blocks == null) Blocks = UnitOfWork.Repository<ProductBlock>().Find(x => true);
        }

        #region Commands

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        #endregion

        protected abstract void AddCommand_Execute();

        private void AddProductIncludedCommand_Execute()
        {
            var productIncludedWrapper = new ProductIncludedWrapper(new ProductIncluded());
            var productsIncludedViewModel = new ProductsIncludedViewModel(productIncludedWrapper, UnitOfWork, Container);
            var dr = Container.Resolve<IDialogService>().ShowDialog(productsIncludedViewModel);
            if(!dr.HasValue || !dr.Value) return;
            Groups.SelectedGroup.AddProductIncluded(productsIncludedViewModel.ViewModel.Entity, productsIncludedViewModel.IsForEach);
            RefreshPrice(Groups.SelectedGroup);
        }

        private void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            Groups.SelectedGroup.RemoveProductIncluded(SelectedProductIncluded);
            RefreshPrice(Groups.SelectedGroup);
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            //удаление из группы
            if (Groups.Contains(Groups.SelectedGroup))
            {
                Groups.Remove(Groups.SelectedGroup);
            }
            //удаление из подгруппы
            else
            {
                var group = Groups.Single(x => x.Groups != null && x.Groups.Contains(Groups.SelectedGroup as TMember));
                group.Groups.Remove(Groups.SelectedGroup as TMember);

                //если группа стала пустая - удалить
                if (!group.Groups.Any())
                {
                    Groups.Remove(group);
                }
            }

            Groups.SelectedGroup = default(TGroup);
        }

        private async void ChangeProductCommand_Execute(TGroup wrappersGroup)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product.Id) return;
            product = await UnitOfWork.Repository<Product>().GetByIdAsync(product.Id);
            wrappersGroup.Product = new ProductWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private async void ChangeFacilityCommand_Execute(TGroup wrappersGroup)
        {
            var facilities = await UnitOfWork.Repository<Facility>().GetAllAsNoTrackingAsync();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Id);
            if (facility == null) return;
            facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(facility.Id);
            wrappersGroup.Facility = new FacilityWrapper(facility);
        }

        private async void ChangePaymentsCommand_Execute(TGroup wrappersGroup)
        {
            var sets = await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Id);
            if (set == null) return;
            set = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetWrapper(set);
        }

        #endregion

        public bool IsValid => Groups != null && Groups.Any() && Groups.IsValid;

        public bool IsChanged => Groups != null && Groups.IsChanged;

        public event Action GroupChanged;

        #region Accept

        public virtual void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = GetAddedUnits().ToList();
            UnitOfWork.Repository<TModel>().AddRange(added);

            //удаляем удаленные
            var removedModels = GetRemovedUnits().ToList();
            //сообщаем об удалении (так высоко, т.к. после удаления объект рушится)
            removedModels.ForEach(x => eventAggregator.GetEvent<TAfterRemoveEvent>().Publish(x));
            UnitOfWork.Repository<TModel>().DeleteRange(removedModels);

            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).Cast<TGroup>().ToList();
            modified = modified.Concat(Groups.ModifiedItems).ToList();

            Groups.AcceptChanges();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<TAfterSaveEvent>().Publish(x));
        }

        protected IEnumerable<TModel> GetAddedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).Cast<TGroup>();
            added = added.Concat(Groups.AddedItems);
            return added.Select(x => x.Model).Distinct();
        }

        protected IEnumerable<TModel> GetRemovedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).Cast<TGroup>().ToList();
            added = added.Concat(Groups.AddedItems).ToList();

            //удаляем удаленные
            var removed = Groups.Except(added).Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).Cast<TGroup>().ToList();
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed = removed.Concat(Groups.RemovedItems.Where(x => x.Groups != null).SelectMany(x => x.Groups).Cast<TGroup>()).ToList();
            return removed.Select(x => x.Model).Distinct().ToList();
        }

        #endregion

    }
}