using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public partial class ProjectUnitGroupsViewModel : ViewModelBase
    {
        private ProjectWrapper _projectWrapper;

        public ProjectUnitGroupsCollection Groups { get; protected set; } = new ProjectUnitGroupsCollection(new List<ProjectUnitGroup>(), false);

        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public ICommand AddProductIncludedCommand { get; }
        public ICommand RemoveProductIncludedCommand { get; }

        public ICommand ChangeProducerCommand { get; }

        #endregion

        public ProjectUnitGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(
                () =>
                {
                    if (!((IProjectUnit) Groups.SelectedItem).CanRemove)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Удаление невозможно, т.к. это оборудование размещено в производстве.");
                        return;
                    }

                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?", defaultNo: true) == MessageDialogResult.No)
                        return;

                    //удаление из группы
                    if (Groups.SelectedItem is ProjectUnitGroup)
                    {
                        Groups.Remove(Groups.SelectedItem as ProjectUnitGroup);
                    }

                    //удаление из подгруппы
                    if (Groups.SelectedItem is ProjectUnitWrapper)
                    {
                        var unit = Groups.SelectedItem as ProjectUnitWrapper;
                        var group = Groups.Single(x => x.Contains(unit));

                        //если группа стала пустая - удалить
                        if (group.Count == 1)
                        {
                            Groups.Remove(group);
                        }
                        else
                        {
                            group.Remove(unit);
                        }
                    }

                    Groups.SelectedItem = null;

                },
                () => Groups.SelectedItem != null);

            ChangeFacilityCommand = new DelegateCommand<IProjectUnit>(
                unit =>
                {
                    var facilities = UnitOfWork.Repository<Facility>().GetAllAsNoTracking();
                    var facility = Container.Resolve<ISelectService>().SelectItem(facilities, unit.Facility?.Id);
                    if (facility == null) return;
                    facility = UnitOfWork.Repository<Facility>().GetById(facility.Id);
                    unit.Facility = new FacilityWrapper(facility);
                });

            ChangeProductCommand = new DelegateCommand<IProjectUnit>(
                unit =>
                {
                    var product = Container.Resolve<IGetProductService>().GetProduct(unit.Product?.Model);
                    if (product == null || product.Id == unit.Product.Id) return;
                    product = UnitOfWork.Repository<Product>().GetById(product.Id);
                    unit.Product = new ProductWrapper(product);

                    RefreshPrice(unit);
                });

            ChangePaymentsCommand = new DelegateCommand<IProjectUnit>(
                unit =>
                {
                    var sets = UnitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTracking();
                    var set = Container.Resolve<ISelectService>().SelectItem(sets, unit.PaymentConditionSet?.Id);
                    if (set == null) return;
                    set = UnitOfWork.Repository<PaymentConditionSet>().GetById(set.Id);
                    unit.PaymentConditionSet = new PaymentConditionSetWrapper(set);
                });

            ChangeProducerCommand = new DelegateCommand<IProjectUnit>(
                wrappersGroup =>
                {
                    var producers = UnitOfWork.Repository<Company>().Find(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
                    var producer = Container.Resolve<ISelectService>().SelectItem(producers, wrappersGroup.Producer?.Id);
                    if (producer == null) return;
                    producer = UnitOfWork.Repository<Company>().GetById(producer.Id);
                    wrappersGroup.Producer = new CompanyWrapper(producer);
                },
                wrappersGroup => wrappersGroup?.Model.Specification == null);

            #region ProductIncludedCommands

            //добавление включенного оборудования
            AddProductIncludedCommand = new DelegateCommand(
                () =>
                {
                    var productIncludedWrapper = new ProductIncludedWrapper(new ProductIncluded());
                    var productsIncludedViewModel = new ProductsIncludedViewModel(productIncludedWrapper, UnitOfWork, Container);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(productsIncludedViewModel);
                    if (!dr.HasValue || !dr.Value) return;
                    Groups.SelectedGroup.AddProductIncluded(productsIncludedViewModel.ViewModel.Entity, productsIncludedViewModel.IsForEach);
                    RefreshPrice(Groups.SelectedGroup);
                },
                () => Groups.SelectedItem != null);

            //удаление включенного оборудования
            RemoveProductIncludedCommand = new DelegateCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?", defaultNo: true) == MessageDialogResult.No)
                        return;

                    Groups.SelectedGroup.RemoveProductIncluded(Groups.SelectedProductIncluded);
                    RefreshPrice(Groups.SelectedGroup);

                },
                () => Groups.SelectedProductIncluded != null);

            #endregion

            Groups.SumChanged += () => { OnPropertyChanged(nameof(Sum)); };
        }

        protected List<ProjectUnitGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units
                .GroupBy(x => x, new SalesUnitsGroupsComparer())
                .OrderByDescending(x => x.Key.Cost)
                .Select(x => new ProjectUnitGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _projectWrapper = parentWrapper;
        }

        #region AddCommand

        protected void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if (_projectWrapper != null) salesUnit.Project = _projectWrapper;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new ProjectUnitGroup(units.ToList());
            Groups.Add(group);
            RefreshPrice(group);
            Groups.SelectedItem = group;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="projectUnitWrapper"></param>
        private void FillingSalesUnit(ProjectUnitWrapper projectUnitWrapper)
        {
            if (Groups.SelectedItem == null)
            {
                var paymentConditionSet = UnitOfWork
                    .Repository<PaymentConditionSet>()
                    .Find(x => x.Id == GlobalAppProperties.Actual.PaymentConditionSet.Id)
                    .First();
                projectUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                projectUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            var selectedUnit = ((IProjectUnit)Groups.SelectedItem).Model;

            projectUnitWrapper.Cost = selectedUnit.Cost;
            projectUnitWrapper.Facility = new FacilityWrapper(selectedUnit.Facility);
            projectUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(selectedUnit.PaymentConditionSet);
            projectUnitWrapper.ProductionTerm = selectedUnit.ProductionTerm;
            projectUnitWrapper.Product = new ProductWrapper(selectedUnit.Product);
            projectUnitWrapper.DeliveryDateExpected = selectedUnit.DeliveryDateExpected;

            //создаем зависимое оборудование
            foreach (var prodIncl in selectedUnit.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Product, Amount = prodIncl.Amount };
                projectUnitWrapper.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
            }
        }

        /// <summary>
        /// Клонирование юнитов по образцу.
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private IEnumerable<SalesUnit> CloneSalesUnits(SalesUnit salesUnit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var unit = (SalesUnit)salesUnit.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                yield return unit;
            }

        }
        #endregion

        public double Sum => Groups.Sum(x => x.Total);


        protected void Load(IEnumerable<SalesUnit> units, IUnitOfWork unitOfWork, bool isNew)
        {
            UnitOfWork = unitOfWork;
            var salesUnits = units as SalesUnit[] ?? units.ToArray();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }

            //создаем контейнер
            Groups = new ProjectUnitGroupsCollection(GetGroups(salesUnits), isNew);
            Groups.SumChanged += () => { OnPropertyChanged(nameof(Sum)); };

            //реакция на выбор группы
            Groups.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)RemoveCommand)?.RaiseCanExecuteChanged();
                ((DelegateCommand)AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(Prices));
            };

            //реакция на выбор включенного оборудования
            Groups.SelectedProductIncludedChanged += productIncluded =>
            {
                ((DelegateCommand)RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
            };

            //событие для того, чтобы вид перепривязал группы
            OnPropertyChanged(nameof(Groups));

            //подписка на события изменения каждой группы и их членов
            ((IValidatableChangeTrackingCollection<ProjectUnitGroup>)Groups).PropertyChanged += (sender, args) => GroupChanged?.Invoke();
            Groups.CollectionChanged += (sender, args) => GroupChanged?.Invoke();

            //обновление прайса каждой группы
            foreach (var group in Groups)
            {
                RefreshPrice(group);
            }
        }

        public bool IsValid => Groups != null && Groups.Any() && Groups.IsValid;

        public bool IsChanged => Groups != null && Groups.IsChanged;

        public event Action GroupChanged;

        #region Accept

        public virtual void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //добавляем созданные
            var added = GetAddedUnits().ToList();
            UnitOfWork.Repository<SalesUnit>().AddRange(added);

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

        protected IEnumerable<SalesUnit> GetAddedUnits()
        {
            var added = Groups.AddedItems.Where(x => x.Any()).SelectMany(x => x);
            added = added.Concat(Groups.Where(x => x.AddedItems.Any()).SelectMany(x => x.AddedItems));
            return added.Select(x => x.Model).Distinct();
        }

        protected IEnumerable<SalesUnit> GetRemovedUnits()
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
