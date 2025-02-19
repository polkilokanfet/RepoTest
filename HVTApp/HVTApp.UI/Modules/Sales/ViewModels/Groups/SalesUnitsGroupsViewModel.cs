using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
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
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public interface IProjectUnit
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        string Comment { get; set; }
        Facility Facility { get; set; }
        Product Product { get; set; }
        PaymentConditionSet PaymentConditionSet { get; set; }
        Company Producer { get; set; }
    }

    public class SalesUnitViewModel : WrapperBase<SalesUnit>, IProjectUnit
    {

        public double Cost
        {
            get => Model.Cost;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public double? CostDelivery
        {
            get => Model.CostDelivery;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public bool IsRemoved
        {
            get => Model.IsRemoved;
            set => SetValue(value);
        }

        public int ProductionTerm
        {
            get => Model.ProductionTerm;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }

        #region Facility

        private Facility _facility;
        public Facility Facility
        {
            get => _facility;
            set => SetProperty(ref _facility, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.FacilityId));
            });
        }

        #endregion

        #region Product

        private Product _product;
        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.ProductId));
            });
        }

        #endregion

        #region PaymentConditionSet

        private PaymentConditionSet _paymentConditionSet;
        public PaymentConditionSet PaymentConditionSet
        {
            get => _paymentConditionSet;
            set => SetProperty(ref _paymentConditionSet, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.PaymentConditionSetId));
            });
        }

        #endregion

        #region Producer

        private Company _producer;
        public Company Producer
        {
            get => _producer;
            set => SetProperty(ref _producer, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.ProducerId));
            });
        }

        #endregion

        public SalesUnitViewModel(SalesUnit model) : base(model)
        {
        }

        public bool HasSameGroup(SalesUnit other)
        {
            return (new ProjectUnitComparer()).Equals(this, new SalesUnitViewModel(other));
        }

        public bool HasSameGroup(SalesUnitViewModel other)
        {
            return (new ProjectUnitComparer()).Equals(this, other);
        }

        public class ProjectUnitComparer : IEqualityComparer<IProjectUnit>
        {
            public virtual bool Equals(IProjectUnit x, IProjectUnit y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (!Equals(x.Cost, y.Cost)) return false;
                if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
                if (!Equals(x.Product.Id, y.Product.Id)) return false;
                if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
                if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
                if (!Equals(x.CostDelivery, y.CostDelivery)) return false;
                if (!Equals(x.Comment, y.Comment)) return false;

                //var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
                //var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

                //if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
                //if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

                return true;
            }

            public int GetHashCode(IProjectUnit obj)
            {
                return 0;
            }
        }
    }


    public class SalesUnitsGroupViewModel : BindableBase, IProjectUnit
    {
        public IValidatableChangeTrackingCollection<SalesUnitViewModel> Units { get; }

        public double Cost
        {
            get => this.Units.First().Cost;
            set => this.Units.ForEach(x => x.Cost = value);
        }

        public double? CostDelivery
        {
            get => this.Units.First().CostDelivery;
            set => this.Units.ForEach(x => x.CostDelivery = value);
        }

        public int ProductionTerm
        {
            get => this.Units.First().ProductionTerm;
            set => this.Units.ForEach(x => x.ProductionTerm = value);
        }
        public string Comment
        {
            get => this.Units.First().Comment;
            set => this.Units.ForEach(x => x.Comment = value);
        }

        #region Facility

        public Facility Facility
        {
            get => Units.First().Facility;
            set
            {
                Units.ForEach(x => x.Facility = value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Product

        public Product Product
        {
            get => Units.First().Product;
            set
            {
                Units.ForEach(x => x.Product = value);
                RaisePropertyChanged();
            }
        }

        public PaymentConditionSet PaymentConditionSet { get; set; }
        public Company Producer { get; set; }

        #endregion

        public SalesUnitsGroupViewModel(IEnumerable<SalesUnitViewModel> salesUnits)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitViewModel>(salesUnits.Select(salesUnit => salesUnit));
        }

        public bool Add(SalesUnit salesUnit)
        {
            if (!this.Units.First().HasSameGroup(salesUnit)) return false;
            this.Units.Add(new SalesUnitViewModel(salesUnit));
            return true;
        }
    }

    public class SalesUnitsGroupViewModelContainer : IEnumerable<SalesUnitsGroupViewModel>, INotifyCollectionChanged
    {
        ObservableCollection<SalesUnitsGroupViewModel> SalesUnitsGroups {get; }
        IValidatableChangeTrackingCollection<SalesUnitViewModel> SalesUnits { get; }

        public bool IsValid => this.SalesUnits.Any() && SalesUnits.All(x => x.IsValid);
        public bool IsChanged => SalesUnits.IsChanged;

        public SalesUnitsGroupViewModelContainer(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitViewModel>(salesUnits.Select(salesUnit => new SalesUnitViewModel(salesUnit)));
            
            var g = this.SalesUnits
                .GroupBy(x => x.Model, new SalesUnitViewModel.ProjectUnitComparer())
                .Select(x => new SalesUnitsGroupViewModel(x));
            this.SalesUnitsGroups = new ObservableCollection<SalesUnitsGroupViewModel>(g);

            this.SalesUnitsGroups.CollectionChanged += (sender, args) =>
            {
                this.CollectionChanged?.Invoke(sender, args);
            };
        }

        public IEnumerator<SalesUnitsGroupViewModel> GetEnumerator()
        {
            return this.SalesUnitsGroups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SalesUnit salesUnit)
        {
            if (this.SalesUnitsGroups.Any(x => x.Add(salesUnit)))
                return;

            this.SalesUnitsGroups.Add(new SalesUnitsGroupViewModel(new []{new SalesUnitViewModel(salesUnit)}));
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }

    
    public class ChangePaymentsCommand : DelegateCommand<IProjectUnit>
    {
        private static IUnitOfWork _unitOfWork;
        private static ISelectService _selectService;

        public ChangePaymentsCommand(IUnitOfWork unitOfWork, ISelectService selectService) : base(ExecuteMethod)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;
        }

        private static void ExecuteMethod(IProjectUnit projectUnit)
        {
            var paymentConditionSets = _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTracking();
            var paymentConditionSet = _selectService.SelectItem(paymentConditionSets, projectUnit.PaymentConditionSet.Id);
            if (paymentConditionSet == null) return;
            projectUnit.PaymentConditionSet = paymentConditionSet;
        }
    }

    public class ChangeFacilityCommand : DelegateCommand<IProjectUnit>
    {
        private static IUnitOfWork _unitOfWork;
        private static ISelectService _selectService;

        public ChangeFacilityCommand(IUnitOfWork unitOfWork, ISelectService selectService) : base(ExecuteMethod)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;
        }

        private static void ExecuteMethod(IProjectUnit projectUnit)
        {
            var facilities = _unitOfWork.Repository<Facility>().GetAllAsNoTracking();
            var facility = _selectService.SelectItem(facilities, projectUnit.Facility.Id);
            if (facility == null) return;
            projectUnit.Facility = facility;
        }
    }

    public class SalesUnitsGroupsViewModel : ViewModelBaseCanExportToExcel
    {
        private ProjectWrapper1 _projectWrapper;

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


        public SalesUnitsGroupViewModelContainer Groups { get; private set; }


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
            this.Groups1 = new SalesUnitsGroupViewModelContainer(units);

            Load(units, unitOfWork, isNew);
            _projectWrapper = projectWrapper1;
            _projectWrapper.PropertyChanged += (sender, args) => { AddCommand.RaiseCanExecuteChanged();};
        }

        protected override DateTime GetPriceDate(ProjectUnitsGroup @group)
        {
            return @group.RealizationDateCalculated;
        }

        #region AddCommand

        protected void AddCommand_Execute()
        {
            //создаем новый юнит и привязываем его к объекту
            var salesUnit = new SalesUnitWrapper(new SalesUnit());
            if(_projectWrapper != null) salesUnit.Project = new ProjectWrapper(_projectWrapper.Model);;

            //создаем модель для диалога
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, UnitOfWork);

            //заполняем юнит начальными данными
            FillingSalesUnit(viewModel.ViewModel.Item);

            //диалог с пользователем
            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value) return;

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
            if (Groups.SelectedGroup == null)
            {
                var paymentConditionSet = UnitOfWork.Repository<PaymentConditionSet>()
                        .Find(conditionSet => conditionSet.Id == GlobalAppProperties.Actual.PaymentConditionSet.Id)
                        .First();
                salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(paymentConditionSet);
                salesUnitWrapper.ProductionTerm = GlobalAppProperties.Actual.StandartTermFromStartToEndProduction;

                return;
            }

            salesUnitWrapper.Cost = Groups.SelectedGroup.Cost;
            salesUnitWrapper.Facility = new FacilityWrapper(Groups.SelectedGroup.Facility.Model);
            salesUnitWrapper.PaymentConditionSet = new PaymentConditionSetWrapper(Groups.SelectedGroup.PaymentConditionSet.Model);
            salesUnitWrapper.ProductionTerm = Groups.SelectedGroup.ProductionTerm;
            salesUnitWrapper.Product = new ProductWrapper(Groups.SelectedGroup.Product.Model);
            salesUnitWrapper.DeliveryDateExpected = Groups.SelectedGroup.DeliveryDateExpected;
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
                    salesUnitWrapper.CostDelivery = Groups.SelectedGroup.CostDelivery / Groups.SelectedGroup.Amount;
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
}