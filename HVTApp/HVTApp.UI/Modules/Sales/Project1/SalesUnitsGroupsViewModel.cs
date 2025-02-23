using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class EditProjectUnitCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;
        private readonly SalesUnitsGroupsViewModel _viewModel;

        #region CanExecute
        public bool CanExecute(object parameter)
        {
            return _viewModel.Groups.SelectedUnit != null;
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public EditProjectUnitCommand(IUnitOfWork unitOfWork, IUnityContainer container, SalesUnitsGroupsViewModel viewModel)
        {
            _unitOfWork = unitOfWork;
            _container = container;
            _viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            var unit = _viewModel.Groups.SelectedUnit;
            var projectUnitViewModel = new ProjectUnitEditViewModel(unit, _unitOfWork, _container.Resolve<ISelectService>());
            _container.Resolve<IDialogService>().Show(projectUnitViewModel);
        }
    }

    public class AddProjectUnitCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;
        private readonly SalesUnitsGroupsViewModel _viewModel;

        #region CanExecute
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public AddProjectUnitCommand(IUnitOfWork unitOfWork, IUnityContainer container, SalesUnitsGroupsViewModel viewModel)
        {
            _unitOfWork = unitOfWork;
            _container = container;
            _viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        #region AddCommand

        protected void AddCommand_Execute()
        {
            //создаем модель для диалога
            var viewModel = new ProjectUnitAddViewModel(_unitOfWork, _container.Resolve<ISelectService>());

            //заполняем начальные данные
            if (_viewModel.Groups.SelectedUnit != null)
            {
                var selectedUnit = _viewModel.Groups.SelectedUnit;
                var viewModelUnit = viewModel.ProjectUnit;

                viewModelUnit.Cost = selectedUnit.Cost;
                viewModelUnit.Comment = selectedUnit.Comment;
                viewModelUnit.CostDelivery = selectedUnit.CostDelivery;
                viewModelUnit.DeliveryDateExpected = selectedUnit.DeliveryDateExpected;
                viewModelUnit.SetFacility(_unitOfWork.Repository<Facility>().GetById(selectedUnit.FacilityId));
            }

            //диалог с пользователем
            var result = _container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (result.HasValue == false || result.Value == false) return;

            //клонируем юниты
            var units = CloneSalesUnits(viewModel.ViewModel.Item.Model, viewModel.Amount);

            var group = new ProjectUnitsGroup(units.ToList());
            Groups.Add(group);
            RefreshPrice(group);
            Groups.SelectedGroup = group;
        }

        /// <summary>
        /// Заполнение юнита по выбранной группе
        /// </summary>
        /// <param name="salesUnitWrapper"></param>
        private void FillingSalesUnit(SalesUnitWrapper salesUnitWrapper)
        {
            if (_viewModel.Groups.SelectedUnit == null)
            {
                var paymentConditionSet = _unitOfWork.Repository<PaymentConditionSet>().GetById(GlobalAppProperties.Actual.PaymentConditionSet.Id);
                salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                salesUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            salesUnitWrapper.Cost = _viewModel.Groups.SelectedUnit.Cost;
            salesUnitWrapper.Facility = new FacilityWrapper(_viewModel.Groups.SelectedUnit.Facility);
            salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(_viewModel.Groups.SelectedUnit.PaymentConditionSet.Model);
            salesUnitWrapper.ProductionTerm = _viewModel.Groups.SelectedUnit.ProductionTerm;
            salesUnitWrapper.Product = new ProductWrapper(_viewModel.Groups.SelectedUnit.Product);
            salesUnitWrapper.DeliveryDateExpected = _viewModel.Groups.SelectedUnit.DeliveryDateExpected;
            if (Groups.SelectedGroup.CostDelivery.HasValue)
            {
                if (Groups.SelectedGroup.Groups != null &&
                    Groups.SelectedGroup.Groups.Any() &&
                    !Groups.SelectedGroup.Groups.First().CostDelivery.HasValue)
                {
                    salesUnitWrapper.CostDelivery = null;
                }
                else
                {
                    salesUnitWrapper.CostDelivery = _viewModel.Groups.SelectedUnit.CostDelivery / _viewModel.Groups.SelectedUnit.Amount;
                }
            }

            //создаем зависимое оборудование
            foreach (var prodIncl in Groups.SelectedGroup.ProductsIncluded)
            {
                var pi = new ProductIncluded { Product = prodIncl.Model.Product, Amount = prodIncl.Model.Amount };
                salesUnitWrapper.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
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

    }

    public class SalesUnitsGroupsViewModel : ViewModelBaseCanExportToExcel
    {
        private ProjectWrapper1 _projectWrapper;

        #region ICommand

        public DelegateLogCommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public DelegateLogCommand RemoveCommand { get; }

        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentsCommand { get; }

        public DelegateLogCommand AddProductIncludedCommand { get; }
        public DelegateLogCommand RemoveProductIncludedCommand { get; }

        public DelegateLogCommand SetCustomFixedPriceCommand { get; }

        #endregion


        public ProjectUnitGroupsContainer Groups { get; private set; }


        #region BaseGroupsViewModel

        /// <summary>
        /// Флаг групповых действий над айтемами
        /// </summary>
        public bool IsGroupActionMode { get; set; } = true;

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


        protected BaseGroupsViewModel(IUnityContainer container) : base(container)
        {
            AddCommand = new DelegateLogCommand(AddCommand_Execute);
            EditCommand = new EditProjectUnitCommand(this.UnitOfWork, container, this);
            RemoveCommand = new DelegateLogCommand(RemoveCommand_Execute, () => Groups.SelectedGroup != null);

            ChangeFacilityCommand = new ChangeFacilityCommand(container.Resolve<IUnitOfWork>(), container.Resolve<ISelectService>());
            ChangeProductCommand = new DelegateCommand<TGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new ChangePaymentsCommand(container.Resolve<IUnitOfWork>(), container.Resolve<ISelectService>());

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
                    if (Container.Resolve<IMessageService>().ConfirmationDialog("Удаление", "Удалить?", defaultNo: true) == false)
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

            Groups.SumChanged += () => { RaisePropertyChanged(nameof(Sum)); };
        }

        protected virtual void RemoveCommand_Execute()
        {
            if (CanRemoveGroup(Groups.SelectedGroup))
            {
                if (Container.Resolve<IMessageService>().ConfirmationDialog("Удаление", "Вы уверены, что хотите удалить это оборудование?", defaultNo: true) == false)
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
            Groups.SumChanged += () => { RaisePropertyChanged(nameof(Sum)); };

            // реакция на выбор группы
            Groups.SelectedGroupChanged += group =>
            {
                RemoveCommand?.RaiseCanExecuteChanged();
                AddProductIncludedCommand?.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(Prices));
                RaisePropertyChanged(nameof(PricesLaborHours));
            };

            // реакция на выбор включенного оборудования
            Groups.SelectedProductIncludedChanged += productIncluded =>
            {
                (RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
                SetCustomFixedPriceCommand?.RaiseCanExecuteChanged();
            };

            // событие для того, чтобы вид перепривязал группы
            RaisePropertyChanged(nameof(Groups));

            // подписка на события изменения каждой группы и их членов
            ((IValidatableChangeTrackingCollection<TGroup>)Groups).PropertyChanged += (sender, args) => GroupChanged?.Invoke();
            Groups.CollectionChanged += (sender, args) => GroupChanged?.Invoke();

            // обновление прайса каждой группы
            Groups.ForEach(RefreshPrice);
        }

        #region Commands

        private void ChangeProductCommand_Execute(TGroup wrappersGroup)
        {
            var product = Container.Resolve<IGetProductService>().GetProduct(wrappersGroup.Product?.Model);
            if (product == null || product.Id == wrappersGroup.Product?.Model.Id) return;
            product = UnitOfWork.Repository<Product>().GetById(product.Id);
            wrappersGroup.Product = new ProductSimpleWrapper(product);
            RefreshPrice(wrappersGroup);

            //если групповое действие
            if (IsGroupActionMode && this.Groups.SelectedGroups != null && this.Groups.SelectedGroups.Length > 1)
            {
                foreach (var selectedGroup in Groups.SelectedGroups)
                {
                    if (selectedGroup is TGroup grp)
                    {
                        if (Equals(grp, wrappersGroup))
                            continue;

                        grp.Product = wrappersGroup.Product;
                        RefreshPrice(grp);
                    }
                }
            }

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
            var removed =
                Groups
                    .Except(added)
                    .Where(x => x.Groups != null)
                    .SelectMany(x => x.Groups.RemovedItems)
                    .Cast<TGroup>()
                    .ToList();
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


        #endregion


        #region Price

        //блоки, необходимые для поиска аналогов
        protected readonly Dictionary<TGroup, Price> PriceDictionary = new Dictionary<TGroup, Price>();

        protected readonly Dictionary<TGroup, Price> PriceDictionaryLaborHours = new Dictionary<TGroup, Price>();

        /// <summary>
        /// Структура себестоимости выбранной группы
        /// </summary>
        public List<Price> Prices => Groups.SelectedGroup == null
            ? null
            : new List<Price> { PriceDictionary[Groups.SelectedGroup] };

        public List<Price> PricesLaborHours => Groups.SelectedGroup == null
            ? null
            : new List<Price> { PriceDictionaryLaborHours[Groups.SelectedGroup] };

        /// <summary>
        /// Дата для расчета себестоимости.
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        protected abstract DateTime GetPriceDate(TGroup grp);

        /// <summary>
        /// Обновление себестоимости группы.
        /// </summary>
        /// <param name="grp"></param>
        protected void RefreshPrice(TGroup grp)
        {
            if (grp == null) return;

            this.RefreshPriceLaborHours(grp);

            //срок актуальности
            var priceTerm = GlobalAppProperties.Actual.ActualPriceTerm;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionary.ContainsKey(grp))
                PriceDictionary.Add(grp, null);

            //обновляем структуру себестоимости этой группе
            PriceDictionary[grp] = GlobalAppProperties.PriceService.GetPrice(grp.Model, GetPriceDate(grp), true);

            //обновляем себестоимость группы
            grp.Price = PriceDictionary[grp].SumPriceTotal;
            grp.FixedCost = PriceDictionary[grp].SumFixedTotal;

            //основная з/п
            var primaryPayment = PriceDictionary[grp].LaborHoursTotal * GlobalAppProperties.PriceService.GetLaborHoursCost(GetPriceDate(grp));
            //отчисления
            var dif = primaryPayment * 30.7 / 100.0;
            //резерв отпусков
            var vac = (primaryPayment + dif) * 7.7 / 100;
            //фонд оплаты труда
            grp.WageFund = primaryPayment + dif + vac;

            RaisePropertyChanged(nameof(Prices));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }

        private void RefreshPriceLaborHours(TGroup grp)
        {
            if (grp == null) return;

            //если в словаре нет такой группы, добавляем её
            if (!PriceDictionaryLaborHours.ContainsKey(grp))
                PriceDictionaryLaborHours.Add(grp, null);

            //обновляем структуру себестоимости этой группе
            PriceDictionaryLaborHours[grp] = GlobalAppProperties.PriceService.GetPrice(grp.Model, GetPriceDate(grp), false);

            //обновляем себестоимость группы
            grp.Price = PriceDictionaryLaborHours[grp].SumPriceTotal;
            grp.FixedCost = PriceDictionaryLaborHours[grp].SumFixedTotal;

            //основная з/п
            var primaryPayment = PriceDictionaryLaborHours[grp].LaborHoursTotal * GlobalAppProperties.PriceService.GetLaborHoursCost(GetPriceDate(grp));
            //отчисления
            var dif = primaryPayment * 30.7 / 100.0;
            //резерв отпусков
            var vac = (primaryPayment + dif) * 7.7 / 100;
            //фонд оплаты труда
            grp.WageFund = primaryPayment + dif + vac;

            RaisePropertyChanged(nameof(PricesLaborHours));

            //если в группе есть зависимые группы - обновить и для них
            grp.Groups?.ForEach(x => RefreshPrice(x as TGroup));
        }


        #endregion

        protected override bool CanRemoveGroup(ProjectUnitsGroup targetGroup)
        {
            if (targetGroup.CanRemove == false)
            {
                Container.Resolve<IMessageService>().Message("Удаление невозможно", $"Оборудованию ({targetGroup}) присвоен заводской заказ.");
                return targetGroup.CanRemove;
            }
            return true;
        }

        protected override void RemoveGroup(ProjectUnitsGroup targetGroup)
        {
            var salesUnits = targetGroup.Groups == null
                ? new List<SalesUnit> {targetGroup.SalesUnit}
                : new List<SalesUnit>(targetGroup.Groups.Select(projectUnitsGroup => projectUnitsGroup.SalesUnit));

            //если ни один юнит ещё не сохранен в БД
            if (salesUnits.All(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id) == null))
            {
                base.RemoveGroup(targetGroup);
                return;
            }

            //проверяем не включено ли оборудование в какой-либо бюджет
            var budgetUnits = UnitOfWork.Repository<BudgetUnit>().Find(budgetUnit => budgetUnit.IsRemoved == false);
            var idIntersection = salesUnits
                .Select(salesUnit => salesUnit.Id)
                .Intersect(budgetUnits.Select(budgetUnit => budgetUnit.SalesUnit.Id))
                .ToList();
            if (idIntersection.Any())
            {
                var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Это оборудование включено в бюджет. Вы уверены, что хотите удалить его?");

                if (dr)
                    salesUnits.Where(salesUnit => idIntersection.Contains(salesUnit.Id)).ForEach(salesUnit => salesUnit.IsRemoved = true);
                else
                    return;
            }

            //проверка на включение в задачи ТСП
            var salesUnitsInTasks = UnitOfWork.Repository<PriceEngineeringTask>()
                .Find(priceEngineeringTask => priceEngineeringTask.SalesUnits.Any())
                .SelectMany(priceEngineeringTask => priceEngineeringTask.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTasks))
            {
                salesUnit.IsRemoved = true;
            }

            //проверка на включение в задачи TCE
            var salesUnitsInTce = UnitOfWork.Repository<TechnicalRequrements>()
                .Find(technicalRequrements => technicalRequrements.SalesUnits.Any())
                .SelectMany(technicalRequrements => technicalRequrements.SalesUnits);
            foreach (var salesUnit in salesUnits.Intersect(salesUnitsInTce))
            {
                salesUnit.IsRemoved = true;
            }

            UnitOfWork.Repository<SalesUnit>().DeleteRange(salesUnits.Where(salesUnit => salesUnit.IsRemoved == false));
            base.RemoveGroup(targetGroup);
        }

        protected override IEnumerable<SalesUnit> GetUnitsForTotalRemove()
        {
            return base.GetUnitsForTotalRemove().Where(salesUnit => !salesUnit.IsRemoved);
        }

        /// <summary>
        /// Изменить производителя
        /// </summary>
        public ICommand ChangeProducerCommand { get; }

        /// <summary>
        /// Удалить производителя
        /// </summary>
        public ICommand RemoveProducerCommand { get; }

        /// <summary>
        /// Перенести оборудование в другой проект
        /// </summary>
        public DelegateLogCommand ChangeProjectCommand { get; }

        public SalesUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
            ChangeProjectCommand = new DelegateLogCommand(
                () =>
                {
                    var projectUnitsGroup = Groups.SelectedGroup;

                    var projects = UnitOfWork.Repository<Project>().Find(project1 => project1.Manager.Id == GlobalAppProperties.User.Id);
                    var project = Container.Resolve<ISelectService>().SelectItem(projects);
                    if (project == null) return;
                    project = UnitOfWork.Repository<Project>().GetById(project.Id);
                    projectUnitsGroup.Project = new ProjectSimpleWrapper(project);
                    base.RemoveGroup(projectUnitsGroup);

                    ChangeProjectCommand.RaiseCanExecuteChanged();
                },
                () => true);

            ChangeProducerCommand = new DelegateCommand<ProjectUnitsGroup>(
                projectUnitsGroup =>
                {
                    var producers = UnitOfWork.Repository<Company>().Find(company => company.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
                    var producer = Container.Resolve<ISelectService>().SelectItem(producers, projectUnitsGroup.Producer?.Model.Id);
                    if (producer == null) return;
                    producer = UnitOfWork.Repository<Company>().GetById(producer.Id);
                    projectUnitsGroup.Producer = new CompanySimpleWrapper(producer);
                    ((DelegateCommand<ProjectUnitsGroup>)RemoveProducerCommand).RaiseCanExecuteChanged();

                    //если групповое действие
                    if (IsGroupActionMode && this.Groups.SelectedGroups != null && this.Groups.SelectedGroups.Length > 1)
                    {
                        foreach (var selectedGroup in Groups.SelectedGroups)
                        {
                            if (selectedGroup is ProjectUnitsGroup grp)
                            {
                                if (Equals(grp, projectUnitsGroup))
                                    continue;

                                if (grp.Specification == null)
                                    grp.Producer = projectUnitsGroup.Producer;
                            }
                        }
                    }

                },
                projectUnitsGroup => projectUnitsGroup?.Specification == null);

            RemoveProducerCommand = new DelegateCommand<ProjectUnitsGroup>(
                projectUnitsGroup =>
                {
                    projectUnitsGroup.Producer = null;
                    ((DelegateCommand<ProjectUnitsGroup>)RemoveProducerCommand).RaiseCanExecuteChanged();
                },
                projectUnitsGroup => projectUnitsGroup?.Specification == null && projectUnitsGroup?.Producer != null);
        }

        protected override List<ProjectUnitsGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units.GroupBy(salesUnit => salesUnit, new SalesUnitsGroupsComparer())
                .OrderBy(x => x.Key, new ProductCostComparer())
                .Select(x => new ProjectUnitsGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, ProjectWrapper1 projectWrapper1, IUnitOfWork unitOfWork, bool isNew)
        {
            this.Groups1 = new ProjectUnitGroupsContainer(units);

            Load(units, unitOfWork, isNew);
            _projectWrapper = projectWrapper1;
            _projectWrapper.PropertyChanged += (sender, args) => { AddCommand.RaiseCanExecuteChanged();};
        }

        protected override DateTime GetPriceDate(ProjectUnitsGroup @group)
        {
            return @group.RealizationDateCalculated;
        }

    }
}