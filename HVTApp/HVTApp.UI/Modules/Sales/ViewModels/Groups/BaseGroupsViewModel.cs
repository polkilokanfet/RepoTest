using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public abstract partial class BaseGroupsViewModel<TGroup, TMember, TModel, TAfterSaveEvent, TAfterRemoveEvent> : ViewModelBaseCanExportToExcel
        where TModel : class, IUnit
        where TMember : class, IGroupValidatableChangeTracking<TModel>
        where TGroup : class, IGroupValidatableChangeTrackingWithCollection<TMember, TModel>
        where TAfterSaveEvent : PubSubEvent<TModel>, new()
        where TAfterRemoveEvent : PubSubEvent<TModel>, new()
    {
        public GroupsCollection<TModel, TGroup, TMember> Groups { get; protected set; } = new GroupsCollection<TModel, TGroup, TMember>(new List<TGroup>(), false);

        public double Sum
        {
            get { return Groups.Sum(x => x.Total); }
            set
            {
                //распределение суммы по всем юнитам равномерно
                
                if (!Groups.Any()) return;

                if (value <= 0) return;

                var totalWithout = value 
                    - Groups.Sum(x => x.FixedCost * x.Amount) 
                    - Groups.Sum(x => x.CostDelivery ?? 0);

                if (totalWithout <= 0) return;

                var priceTotal = Groups.Sum(x => x.Price * x.Amount);

                foreach (var grp in Groups)
                {
                    double deliveryCost = grp.CostDelivery ?? 0;
                    grp.Cost = grp.FixedCost + deliveryCost / grp.Amount + totalWithout * (grp.Price) / priceTotal;
                }
            }
        }

        protected abstract List<TGroup> GetGroups(IEnumerable<TModel> units);

        #region ICommand

        public DelegateLogCommand AddCommand { get; private set; }
        public DelegateLogCommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public DelegateLogCommand AddProductIncludedCommand { get; }
        public DelegateLogCommand RemoveProductIncludedCommand { get; }

        public DelegateLogCommand SetCustomFixedPriceCommand { get; }

        #endregion

        protected BaseGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateLogCommand(AddCommand_Execute, AddCommand_CanExecute);
            RemoveCommand = new DelegateLogCommand(RemoveCommand_Execute, () => Groups.SelectedGroup != null);

            ChangeFacilityCommand = new DelegateCommand<TGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<TGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<TGroup>(ChangePaymentsCommand_Execute);

            #region ProductIncludedCommands

            //добавление включенного оборудования
            AddProductIncludedCommand = new DelegateLogCommand(
                () =>
                {
                    var productIncludedWrapper = new ProductIncludedWrapper(new ProductIncluded());
                    var productsIncludedViewModel = new ProductsIncludedViewModel(productIncludedWrapper, UnitOfWork, Container);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(productsIncludedViewModel);
                    if (!dr.HasValue || !dr.Value) return;
                    Groups.SelectedGroup.AddProductIncluded(productsIncludedViewModel.ViewModel.Entity, productsIncludedViewModel.IsForEach);
                    RefreshPrice(Groups.SelectedGroup);
                }, 
                () => Groups.SelectedGroup != null);

            //удаление включенного оборудования
            RemoveProductIncludedCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?", defaultNo: true) == MessageDialogResult.No)
                        return;

                    Groups.SelectedGroup.RemoveProductIncluded(Groups.SelectedProductIncluded);
                    RefreshPrice(Groups.SelectedGroup);

                },
                () => Groups.SelectedProductIncluded != null);

            //установка нестандартной себестоимости шеф-монтажа
            SetCustomFixedPriceCommand = new DelegateLogCommand(
                () =>
                {
                    //шеф-монтажи, которые подлежат изменению
                    var productsIncludedTarget = Groups
                        .Where(x => x.Groups != null)
                        .SelectMany(x => x.Groups)
                        .SelectMany(x => x.ProductsIncluded)
                        .Where(x => Equals(x.Model.Id, Groups.SelectedProductIncluded.Model.Id))
                        .ToList();
                    
                    var productIncluded = productsIncludedTarget.Any() 
                        ? productsIncludedTarget.Select(x => x.Model).Distinct().Single()
                        : Groups.SelectedProductIncluded.Model;

                    var original = productIncluded.CustomFixedPrice;

                    var viewModel = new SupervisionPriceViewModel(new ProductIncludedWrapper(productIncluded), UnitOfWork, Container);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(viewModel);
                    if (dr.HasValue || dr.Value == true)
                    {
                        productsIncludedTarget.ForEach(x => x.CustomFixedPrice = productIncluded.CustomFixedPrice);
                    }
                    else
                    {
                        productsIncludedTarget.ForEach(x => x.CustomFixedPrice = original);
                    }

                    if (!Equals(productIncluded.CustomFixedPrice, original))
                    {
                        RefreshPrice(Groups.SelectedGroup);
                    }
                },
                () => Groups.SelectedProductIncluded != null && Groups.SelectedProductIncluded.Model.Product.ProductBlock.IsSupervision);

            #endregion

            Groups.SumChanged += () => { OnPropertyChanged(nameof(Sum)); };
        }

        protected virtual void RemoveCommand_Execute()
        {
            if (CanRemoveGroup(Groups.SelectedGroup))
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены, что хотите удалить это оборудование?", defaultNo: true) != MessageDialogResult.Yes)
                {
                    return;
                }

                RemoveGroup(Groups.SelectedGroup);
                Groups.SelectedGroup = default(TGroup);
            }
        }

        protected virtual void RemoveGroup(TGroup targetGroup)
        {
            //удаление из группы
            if (Groups.Contains(targetGroup))
            {
                Groups.Remove(targetGroup);
            }
            //удаление из подгруппы
            else
            {
                var parentGroup = Groups.Single(x => x.Groups != null && x.Groups.Contains(targetGroup as TMember));
                parentGroup.Groups.Remove(targetGroup as TMember);

                //если группа стала пустая - удалить
                if (!parentGroup.Groups.Any())
                {
                    Groups.Remove(parentGroup);
                }
            }
        }

        /// <summary>
        /// Можно ли удалять группу?
        /// </summary>
        /// <param name="targetGroup"></param>
        /// <returns></returns>
        protected virtual bool CanRemoveGroup(TGroup targetGroup)
        {
            return true;
        }

        protected void Load(IEnumerable<TModel> units, IUnitOfWork unitOfWork, bool isNew)
        {
            UnitOfWork = unitOfWork;
            var unitsArray = units as TModel[] ?? units.ToArray();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = unitsArray.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = unitsArray.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }

            //создаем контейнер
            Groups = new GroupsCollection<TModel, TGroup, TMember>(GetGroups(unitsArray), isNew);
            Groups.SumChanged += () => { OnPropertyChanged(nameof(Sum)); };

            // реакция на выбор группы
            Groups.SelectedGroupChanged += group =>
            {
                (RemoveCommand)?.RaiseCanExecuteChanged();
                (AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(Prices));
            };

            // реакция на выбор включенного оборудования
            Groups.SelectedProductIncludedChanged += productIncluded =>
            {
                (RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
                SetCustomFixedPriceCommand?.RaiseCanExecuteChanged();
            };

            // событие для того, чтобы вид перепривязал группы
            OnPropertyChanged(nameof(Groups));

            // подписка на события изменения каждой группы и их членов
            ((IValidatableChangeTrackingCollection<TGroup>)Groups).PropertyChanged += (sender, args) => GroupChanged?.Invoke();
            Groups.CollectionChanged += (sender, args) => GroupChanged?.Invoke();

            // обновление прайса каждой группы
            Groups.ForEach(RefreshPrice);
        }

        private void GrpOnProductIncludedIsChanged()
        {
        }

        #region Commands

        protected abstract void AddCommand_Execute();

        protected virtual bool AddCommand_CanExecute()
        {
            return true;
        }

        private void ChangeProductCommand_Execute(TGroup wrappersGroup)
        {
            var product = Container.Resolve<IGetProductService>().GetProduct(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product?.Model.Id) return;
            product = UnitOfWork.Repository<Product>().GetById(product.Id);
            wrappersGroup.Product = new ProductSimpleWrapper(product);
            RefreshPrice(wrappersGroup);
        }

        private void ChangeFacilityCommand_Execute(TGroup wrappersGroup)
        {
            var facilities = UnitOfWork.Repository<Facility>().GetAllAsNoTracking();
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities, wrappersGroup.Facility?.Model.Id);
            if (facility == null) return;
            facility = UnitOfWork.Repository<Facility>().GetById(facility.Id);
            wrappersGroup.Facility = new FacilitySimpleWrapper(facility);
        }

        private void ChangePaymentsCommand_Execute(TGroup wrappersGroup)
        {
            var sets = UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTracking();
            var set = Container.Resolve<ISelectService>().SelectItem(sets, wrappersGroup.PaymentConditionSet?.Model.Id);
            if (set == null) return;
            set = UnitOfWork.Repository<PaymentConditionSet>().GetById(set.Id);
            wrappersGroup.PaymentConditionSet = new PaymentConditionSetSimpleWrapper(set);
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
            var removedModels = GetUnitsForTotalRemove().ToList();
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

        protected virtual IEnumerable<TModel> GetUnitsForTotalRemove()
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

        /// <summary>
        /// Округлание цен
        /// </summary>
        public void RoundUpGroupsCosts(double roundUpAccuracy)
        {
            foreach (var grp in Groups)
            {
                grp.Cost = Math.Ceiling(grp.Cost / roundUpAccuracy) * roundUpAccuracy;
            }
        }
    }
}