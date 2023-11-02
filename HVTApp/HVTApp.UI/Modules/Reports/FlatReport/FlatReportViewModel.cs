using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.Reports.FlatReport.Comparator;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;
using HVTApp.UI.Modules.Reports.FlatReport.Reports;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportViewModel : ViewModelBaseCanExportToExcel
    {
        #region Fields


        /// <summary>
        /// не обновлять контейнеры (отображение). Флаг нужен для ускорения работы инвертирования
        /// </summary>
        private bool _doNotRefreshContainersVisualisation = false;
        private List<SalesUnit> _salesUnits;
        private DateTime _startDate;
        private DateTime _finishDate;
        private double _accuracy = 5;
        private FlatReportItem _selectedItem;

        #endregion

        #region Props

        /// <summary>
        /// Только отчетное оборудование
        /// </summary>
        public bool IsReportUnitsOnly { get; set; } = true;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value > FinishDate) return;
                _startDate = value;
                //нужно для создания контейнеров
                GetYearContainerOit(StartDate.Year);
                RefreshInReportStatus();
                RaisePropertyChanged();
            }
        }

        public DateTime FinishDate
        {
            get => _finishDate;
            set
            {
                if (value < StartDate) return;
                _finishDate = value;
                //нужно для создания контейнеров
                GetYearContainerOit(FinishDate.Year);
                RefreshInReportStatus();
                RaisePropertyChanged();
            }
        }

        private void RefreshInReportStatus()
        {
            _doNotRefreshContainersVisualisation = true;

            int monthsCount = MonthContainersOit.Count();
            var startOit = new DateTime(StartDate.Year, StartDate.Month, 1);
            var finishOit = new DateTime(FinishDate.Year, FinishDate.Month, DateTime.DaysInMonth(FinishDate.Year, FinishDate.Month));
            var monthContainersOit = YearContainersOit.SelectMany(containerYear => containerYear.MonthContainers).ToList();
            monthContainersOit.Where(containerMonth => !containerMonth.Date.BetweenDates(startOit, finishOit)).ForEach(containerMonth => containerMonth.InReport = false);
            monthContainersOit.Where(containerMonth => containerMonth.Date.BetweenDates(startOit, finishOit)).ForEach(containerMonth => containerMonth.InReport = true);

            Items.Where(flatReportItem => !flatReportItem.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate)).ForEach(flatReportItem => flatReportItem.InReport = false);

            if(monthsCount != MonthContainersOit.Count())
                RefreshTargetSums();

            RefreshInReportStatusRealization();

            _doNotRefreshContainersVisualisation = false;
            RefreshContainersVisualisation();
        }

        private void RefreshInReportStatusRealization()
        {
            if (Items.All(flatReportItem => !flatReportItem.InReport))
                return;

            var startRealization = Items.Where(flatReportItem => flatReportItem.InReport).Min(flatReportItem => flatReportItem.EstimatedRealizationDate);
            startRealization = new DateTime(startRealization.Year, startRealization.Month, 1);
            var finishRealization = Items.Where(flatReportItem => flatReportItem.InReport).Max(flatReportItem => flatReportItem.EstimatedRealizationDate);
            finishRealization = new DateTime(finishRealization.Year, finishRealization.Month, DateTime.DaysInMonth(finishRealization.Year, finishRealization.Month));
            var monthContainersRealization = YearContainersRealization.SelectMany(containerYear => containerYear.MonthContainers).ToList();
            monthContainersRealization.Where(containerMonth => !containerMonth.Date.BetweenDates(startRealization, finishRealization)).ForEach(x => x.InReport = false);
            monthContainersRealization.Where(containerMonth => containerMonth.Date.BetweenDates(startRealization, finishRealization)).ForEach(x => x.InReport = true);
        }

        /// <summary>
        /// Точность выравнивания (в процентах)
        /// </summary>
        public double Accuracy
        {
            get => _accuracy;
            set
            {
                if (Equals(_accuracy, value)) return;
                if(value < 0) return;
                _accuracy = value;
                YearContainersOit.ForEach(x => x.Accuracy = value / 100.0);
            }
        }

        public ObservableCollection<FlatReportItem> Items { get; } = new ObservableCollection<FlatReportItem>();

        public FlatReportItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                ((DelegateCommand<string>)AddMonthToOitCommand).RaiseCanExecuteChanged();
                ((DelegateCommand<string>)AddMonthToRealizationCommand).RaiseCanExecuteChanged();
                (ExplodeItemCommand).RaiseCanExecuteChanged();
                (ChangeInReportStatusCommand).RaiseCanExecuteChanged();
            }
        }

        public object[] SelectedItems { get; set; }

        #region OrderInTake

        public ObservableCollection<ContainerYear> YearContainersOit { get; } = new ObservableCollection<ContainerYear>();

        public IEnumerable<ContainerMonth> MonthContainersOit => 
            YearContainersOit
                .SelectMany(containerYear => containerYear.MonthContainers)
                .Where(containerMonth => containerMonth.InReport)
                .OrderBy(containerMonth => containerMonth.Year)
                .ThenBy(containerMonth => containerMonth.Month);

        public List<FlatReportItemManagerContainer> ManagerContainersOit => Items
            .Where(flatReportItem => flatReportItem.InReport)
            .GroupBy(flatReportItem => new {flatReportItem.Manager, flatReportItem.EstimatedOrderInTakeDate.Year})
            .Select(flatReportItems => new FlatReportItemManagerContainer(flatReportItems))
            .OrderBy(flatReportItemManagerContainer => flatReportItemManagerContainer.Manager)
            .ThenBy(flatReportItemManagerContainer => flatReportItemManagerContainer.Year).ToList();

        #endregion

        #region Realization

        public ObservableCollection<ContainerYear> YearContainersRealization { get; } = new ObservableCollection<ContainerYear>();

        public List<ContainerMonth> MonthContainersRealization => YearContainersRealization.SelectMany(x => x.MonthContainers).Where(x => x.InReport).OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();

        public List<FlatReportItemManagerContainer> ManagerContainersRealization => Items
            .Where(x => x.InReport)
            .GroupBy(x => new {x.Manager, x.EstimatedRealizationDate.Year})
            .Select(x => new FlatReportItemManagerContainer(x))
            .OrderBy(x => x.Manager)
            .ThenBy(x => x.Year).ToList();

        #endregion

        #region Commands

        /// <summary>
        /// Перезагрузить исходные данные
        /// </summary>
        public DelegateLogCommand ReloadCommand { get; set; }

        /// <summary>
        /// Выровнять данные
        /// </summary>
        public DelegateLogCommand AlignCommand { get; }

        /// <summary>
        /// Свормировать отчет Бюджет
        /// </summary>
        public DelegateLogCommand MakeSalesReportCommand { get; }

        /// <summary>
        /// Свормировать отчет Поступления
        /// </summary>
        public DelegateLogCommand MakePaymentsReportCommand { get; }

        /// <summary>
        /// Прибавить месяц к ОИТ
        /// </summary>
        public ICommand AddMonthToOitCommand { get; }

        /// <summary>
        /// Прибавить месяц к Реализации
        /// </summary>
        public ICommand AddMonthToRealizationCommand { get; }

        /// <summary>
        /// Сохранить данные в бюджет
        /// </summary>
        public DelegateLogCommand SaveBudgetCommand { get; }

        /// <summary>
        /// Сравнить с бюджетом
        /// </summary>
        public DelegateLogCommand CompareBudgetCommand { get; }

        /// <summary>
        /// Загрузить бюджет
        /// </summary>
        public DelegateLogCommand LoadBudgetCommand { get; }

        /// <summary>
        /// Взорвать айтем
        /// </summary>
        public DelegateLogCommand ExplodeItemCommand { get; }

        /// <summary>
        /// Загрузка стандартных цен и ПЗ
        /// </summary>
        public DelegateLogCommand LoadDefaultCostsAndPricesCommand { get; }

        public DelegateLogCommand ChangeInReportStatusCommand { get; }

        #endregion
        
        #endregion

        public FlatReportViewModel(IUnityContainer container) : base(container)
        {
            Items.CollectionChanged += (sender, args) =>
            {
                //если новые айтемы
                if (args.NewItems != null)
                {
                    foreach (var item in args.NewItems.Cast<FlatReportItem>())
                    {
                        if (item.InReport)
                        {
                            AddItemToContainerOit(item);
                            AddItemToContainerRealization(item);                            
                        }
                        Subscribes(item); //подписка на событие изменения ключевых параметров
                    }
                }

                //если удаленные айтемы
                if (args.OldItems != null)
                {
                    foreach (var item in args.OldItems.Cast<FlatReportItem>())
                    {
                        RemoveItemFromContainerOit(item);
                        RemoveItemFromContainerRealization(item);
                        Unsubscribes(item); //отписка от событий изменений ключевых параметров
                    }
                }
            };

            ReloadCommand = new DelegateLogCommand(LoadDefault);

            #region Budget

            CompareBudgetCommand = new DelegateLogCommand(
                () =>
                {
                    var budgets = UnitOfWork.Repository<Budget>().GetAll();
                    var budget = Container.Resolve<ISelectService>().SelectItem(budgets);
                    if (budget != null)
                    {
                        var viewModel = new BudgetComparisionViewModel(Items, budget.Units, Container);
                        Container.Resolve<IDialogService>().Show(viewModel, $"Сравнение с бюджетом от {budget.Date.ToShortDateString()} {budget.Date.ToShortTimeString()}");
                    }
                });

            SaveBudgetCommand = new DelegateLogCommand(
                () =>
                {
                    var budget = new Budget
                    {
                        DateStart = StartDate,
                        DateFinish = FinishDate,
                        Date = DateTime.Now,
                        Name = $"Дата: {DateTime.Today.ToShortDateString()}, время: {DateTime.Now.ToShortTimeString()}"
                    };

                    foreach (var flatReportItem in Items)
                    {
                        foreach (var salesUnit in flatReportItem.SalesUnits)
                        {
                            var budgetUnit = new BudgetUnit
                            {
                                SalesUnit = salesUnit,
                                Cost = flatReportItem.EstimatedCost,
                                CostByManager = salesUnit.Cost,
                                OrderInTakeDate = flatReportItem.EstimatedOrderInTakeDate,
                                OrderInTakeDateByManager = salesUnit.OrderInTakeDate,
                                RealizationDate = flatReportItem.EstimatedRealizationDate,
                                RealizationDateByManager = salesUnit.RealizationDateCalculated,
                                PaymentConditionSet = flatReportItem.EstimatedPaymentConditionSet,
                                PaymentConditionSetByManager = salesUnit.PaymentConditionSet,
                                IsRemoved = !flatReportItem.InReport
                            };

                            budget.Units.Add(budgetUnit);
                        }
                    }

                    UnitOfWork.SaveChanges();
                    Container.Resolve<IMessageService>()
                        .Message("Информация", $"Бюджет \"{budget}\" успешно сохранен.");
                },
                () => new List<Role> {Role.Admin, Role.Director, Role.ReportMaker}.Contains(GlobalAppProperties.User.RoleCurrent));

            LoadBudgetCommand = new DelegateLogCommand(
                () =>
                {
                    UnitOfWork = Container.Resolve<IUnitOfWork>();
                    var budgets = UnitOfWork.Repository<Budget>().GetAll();
                    var budget = Container.Resolve<ISelectService>().SelectItem(budgets);
                    if (budget != null)
                    {
                        var items = new List<FlatReportItem>();
                        var groups = budget.Units.GroupBy(x => x, new BudgetUnitComparer());
                        foreach (var grp in groups)
                        {
                            var salesUnits = grp.Select(x => x.SalesUnit);
                            var budgetUnit = grp.First();
                            items.Add(new FlatReportItem(salesUnits, !budgetUnit.IsRemoved, budgetUnit.Cost, budgetUnit.OrderInTakeDate, budgetUnit.RealizationDate, budgetUnit.PaymentConditionSet));
                        }

                        LoadBase(items, budget.DateStart, budget.DateFinish);
                    }
                });

            #endregion

            #region MakeReportCommand

            MakeSalesReportCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var salesUnitsIds = Items
                        .Where(flatReportItem => flatReportItem.InReport)
                        .SelectMany(flatReportItem => flatReportItem.SalesUnits)
                        .Select(salesUnit => salesUnit.Id)
                        .ToList();

                    var salesUnits = ((ISalesUnitRepository)unitOfWork.Repository<SalesUnit>()).GetForFlatReportView(salesUnitsIds).ToList();
                    var salesReportViewModel = new SalesReportViewModel(Container, unitOfWork, salesUnits.Select(GetSalesUnitWithInjectedData).ToList());
                    Container.Resolve<IDialogService>().Show(salesReportViewModel, $"Бюджет. Момент формирования отчета: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                });

            MakePaymentsReportCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var salesUnits = Items.Where(flatReportItem => flatReportItem.InReport).Where(x => !x.IsLoosen).SelectMany(x => x.SalesUnits).ToList();
                    var salesUnits2 = unitOfWork.Repository<SalesUnit>().Find(x => salesUnits.ContainsById(x)).Select(GetSalesUnitWithInjectedData).ToList();
                    var paymentsPlanViewModel = new PaymentsPlanViewModel(Container, load:false);
                    paymentsPlanViewModel.Load(salesUnits2);

                    Container.Resolve<IDialogService>().Show(paymentsPlanViewModel, $"Поступления. Момент формирования отчета: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                });

            #endregion

            #region AddMonthCommand

            AddMonthToOitCommand = new DelegateCommand<string>(
                parameter =>
                {
                    int monthsAmount;
                    int.TryParse(parameter, out monthsAmount);
                    var selectedItems = SelectedItems.Cast<FlatReportItem>();
                    selectedItems.ForEach(x =>
                    {
                        if(x.AllowEditOit)
                            x.EstimatedOrderInTakeDate = x.EstimatedOrderInTakeDate.AddMonths(monthsAmount);
                    });
                },
                parameter => SelectedItem != null && SelectedItem.AllowEditOit);

            AddMonthToRealizationCommand = new DelegateCommand<string>(
                parameter =>
                {
                    int monthsAmount;
                    int.TryParse(parameter, out monthsAmount);
                    var selectedItems = SelectedItems.Cast<FlatReportItem>();
                    selectedItems.ForEach(x =>
                    {
                        if (x.AllowEditRealization)
                            x.EstimatedRealizationDate = x.EstimatedRealizationDate.AddMonths(monthsAmount);
                    });
                },
                parameter => SelectedItem != null && SelectedItem.AllowEditRealization);

            #endregion

            AlignCommand = new DelegateLogCommand(
                () =>
                {
                    var monthContainers = FlatReportComparator.Align(YearContainersOit.SelectMany(containerYear => containerYear.MonthContainers).Where(containerMonth => containerMonth.InReport)).ToList();
                    monthContainers.Cast<ContainerMonthOit>().ForEach(containerMonthOit => containerMonthOit.FillEstimatedDates());

                    if (monthContainers.Any(containerMonth => !containerMonth.IsOk))
                        Container.Resolve<IMessageService>().Message("Информация", "Не во всех месяцах удалось выровнять суммы с заданной точностью.");
                });

            ExplodeItemCommand = new DelegateLogCommand(
                () =>
                {
                    var item = SelectedItem;
                    var items = item.SalesUnits
                        .Select(salesUnit => new FlatReportItem(new List<SalesUnit> {salesUnit}, item.InReport, item.EstimatedCost,
                            item.EstimatedOrderInTakeDate, item.EstimatedRealizationDate, item.EstimatedPaymentConditionSet));
                    var index = Items.IndexOf(SelectedItem);
                    Items.Remove(SelectedItem);
                    items.ForEach(x => Items.Insert(index, x));
                    SelectedItem = null;
                },
                () => SelectedItem != null);

            LoadDefaultCostsAndPricesCommand = new DelegateLogCommand(
                () =>
                {
                    var defaultCostsAndPrices = UnitOfWork.Repository<ProductCategoryPriceAndCost>().GetAll();
                    var items = Items.Where(flatReportItem => flatReportItem.InReport && !flatReportItem.IsLoosen && flatReportItem.AllowEditOit && flatReportItem.SalesUnit.Specification == null).ToList();
                    foreach (var defaultCostAndPrice in defaultCostsAndPrices)
                    {
                        foreach (var item in items.Where(x => x.SalesUnit.Product.Category.Id == defaultCostAndPrice.Category.Id))
                        {
                            item.EstimatedCost = defaultCostAndPrice.Cost;
                            item.EstimatedPrice = defaultCostAndPrice.Price;
                        }
                    }
                });

            ChangeInReportStatusCommand = new DelegateLogCommand(
                () =>
                {
                    //групповое исключение/включение
                    if (SelectedItems != null && SelectedItems.Any())
                    {
                        _doNotRefreshContainersVisualisation = true;

                        var items = SelectedItems.Cast<FlatReportItem>().ToList();
                        items.ForEach(flatReportItem => flatReportItem.InReport = !flatReportItem.InReport);

                        _doNotRefreshContainersVisualisation = false;

                        RefreshContainersVisualisation();

                    }
                },
                () => SelectedItem != null);

            LoadDefault();
        }

        /// <summary>
        /// Отписка от событий изменений ключевых параметров
        /// </summary>
        /// <param name="item"></param>
        private void Unsubscribes(FlatReportItem item)
        {
            item.InReportIsChanged -= OnInReportIsChanged; //исключение/включение айтема в отчет
            item.EstimatedOrderInTakeMonthIsChanged -= OnEstimatedOrderInTakeMonthIsChanged; //изменение месяца ОИТ в айтеме
            item.EstimatedRealizationMonthIsChanged -= OnEstimatedRealizationMonthIsChanged; //изменение месяца реализации в айтеме
            item.EstimatedCostIsChanged -= OnEstimatedCostIsChanged; //изменение цены в айтеме
        }

        /// <summary>
        /// Подписка на событие изменения ключевых параметров
        /// </summary>
        /// <param name="item"></param>
        private void Subscribes(FlatReportItem item)
        {
            item.InReportIsChanged += OnInReportIsChanged; //исключение/включение айтема в отчет
            item.EstimatedOrderInTakeMonthIsChanged += OnEstimatedOrderInTakeMonthIsChanged; //изменение месяца ОИТ в айтеме
            item.EstimatedRealizationMonthIsChanged += OnEstimatedRealizationMonthIsChanged; //изменение месяца реализации в айтеме
            item.EstimatedCostIsChanged += OnEstimatedCostIsChanged; //изменение цены в айтеме
        }

        #region OnItemIsChanged

        /// <summary>
        /// изменение цены в айтеме
        /// </summary>
        private void OnEstimatedCostIsChanged(FlatReportItem item)
        {
            RefreshTargetSums();
            RefreshContainersVisualisation();
        }

        /// <summary>
        /// изменение месяца реализации в айтеме
        /// </summary>
        /// <param name="item"></param>
        private void OnEstimatedRealizationMonthIsChanged(FlatReportItem item)
        {
            if (item.InReport)
            {
                RemoveItemFromContainerRealization(item);
                AddItemToContainerRealization(item);
                RefreshInReportStatusRealization();
                RaisePropertyChanged(nameof(MonthContainersRealization));
            }
        }

        /// <summary>
        /// изменение месяца ОИТ в айтеме
        /// </summary>
        /// <param name="item"></param>
        private void OnEstimatedOrderInTakeMonthIsChanged(FlatReportItem item)
        {
            if (item.InReport)
            {
                RemoveItemFromContainerOit(item);

                //не вылетел ли айтем из отчета?
                if (!item.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate))
                {
                    item.InReport = false;
                    Container.Resolve<IMessageService>().Message("Информация", $"\"{item}\" вышел за рамки отчетного диапазона дат (по дате ОИТ).");
                }
                else
                {
                    AddItemToContainerOit(item);
                }

                RaisePropertyChanged(nameof(MonthContainersOit));
            }
        }

        /// <summary>
        /// исключение/включение айтема в отчет
        /// </summary>
        /// <param name="item"></param>
        private void OnInReportIsChanged(FlatReportItem item)
        {
            if (item.InReport)
            {
                //добавляем только непроигранные айтемы
                if (!item.IsLoosen)
                {
                    AddItemToContainerOit(item);
                    AddItemToContainerRealization(item);
                }
            }
            else
            {
                RemoveItemFromContainerOit(item);
                RemoveItemFromContainerRealization(item);
            }

            RefreshTargetSums();
            RefreshContainersVisualisation();
        }

        #endregion

        private void LoadDefault()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //загрузка продажных единиц
            _salesUnits = GlobalAppProperties.UserIsManager
                ? ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetForFlatReportView(IsReportUnitsOnly).Where(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()).ToList()
                : ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetForFlatReportView(IsReportUnitsOnly).ToList();

            var items = _salesUnits
                .GroupBy(salesUnit => salesUnit, new SalesUnitsReportComparer())
                //.Select(x => new FlatReportItem(x, x.Key.OrderInTakeDate.BetweenDates(_startDateDefault, _finishDateDefault)))
                .Select(x => new FlatReportItem(x, true))
                .OrderBy(flatReportItem => flatReportItem.EstimatedOrderInTakeDate)
                .ToList();
            
            //расстановка цен в нулевых единицах
            items
                .Where(flatReportItem => Math.Abs(flatReportItem.SalesUnit.Cost) < 0.001)
                .ForEach(flatReportItem => flatReportItem.EstimatedCost = GlobalAppProperties.PriceService.GetPrice(flatReportItem.SalesUnit, flatReportItem.EstimatedOrderInTakeDate, true).SumTotal * 1.4);

            var min = items.Min(flatReportItem => flatReportItem.EstimatedOrderInTakeDate);
            var max = items.Max(flatReportItem => flatReportItem.EstimatedOrderInTakeDate);

            LoadBase(items, new DateTime(min.Year, min.Month, 1), new DateTime(max.Year, max.Month, DateTime.DaysInMonth(max.Year, max.Month)));
        }

        private void LoadBase(List<FlatReportItem> items, DateTime startDate, DateTime finishDate)
        {
            _startDate = startDate;
            _finishDate = finishDate;
            Items.Clear();
            YearContainersOit.Clear();
            YearContainersRealization.Clear();

            //подписка на событие изменения ключевых параметров была в конструкторе

            Items.AddRange(items);

            StartDate = startDate;
            FinishDate = finishDate;

            RaisePropertyChanged(nameof(MonthContainersRealization));
        }

        private void RefreshContainersVisualisation()
        {
            if (_doNotRefreshContainersVisualisation) return;
            RaisePropertyChanged(nameof(MonthContainersOit));
            RaisePropertyChanged(nameof(MonthContainersRealization));
        }

        #region ItemAddRemove

        #region OrderInTake

        /// <summary>
        /// Исключение айтема из контейнера ОИТ
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerOit(FlatReportItem item)
        {
            YearContainersOit.SelectMany(containerYear => containerYear.MonthContainers).SingleOrDefault(containerMonth => containerMonth.Items.Contains(item))?.Items.Remove(item);
        }

        /// <summary>
        /// Добавление айтема в контейнер ОИТ
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToContainerOit(FlatReportItem item)
        {
            //если айтем не попадает в отчетный диапазон дат
            if (!item.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate))
                return;

            GetMonthContainerOit(item.EstimatedOrderInTakeDate).Items.Add(item);
        }

        private ContainerMonth GetMonthContainerOit(DateTime date)
        {
            return GetYearContainerOit(date.Year).MonthContainers.Single(containerMonth => containerMonth.Month == date.Month);
        }

        private ContainerYear GetYearContainerOit(int year)
        {
            var yearContainer = YearContainersOit.SingleOrDefault(containerYear => containerYear.Year == year);
            if (yearContainer == null)
            {
                yearContainer = new ContainerYear(year, Accuracy / 100.0);
                YearContainersOit.Add(yearContainer);
            }

            //создание контейнеров между годами (если они еще не созданы)
            var minYear = YearContainersOit.Min(containerYear => containerYear.Year);
            var maxYear = YearContainersOit.Max(containerYear => containerYear.Year);
            for (int targetYear = minYear; targetYear <= maxYear; targetYear++)
            {
                if (!YearContainersOit.Select(containerYear => containerYear.Year).Contains(targetYear))
                    GetYearContainerOit(targetYear);
            }

            return yearContainer;
        }


        #endregion

        #region Realization

        /// <summary>
        /// Исключение айтема из контейнера Реализации
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerRealization(FlatReportItem item)
        {
            YearContainersRealization.SelectMany(x => x.MonthContainers).SingleOrDefault(x => x.Items.Contains(item))?.Items.Remove(item);
        }

        /// <summary>
        /// Добавление айтема в контейнер Реализации
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToContainerRealization(FlatReportItem item)
        {
            GetMonthContainerRealization(item.EstimatedRealizationDate).Items.Add(item);
        }

        private ContainerMonth GetMonthContainerRealization(DateTime date)
        {
            return GetYearContainerRealization(date.Year).MonthContainers.Single(x => x.Month == date.Month);
        }

        private ContainerYear GetYearContainerRealization(int year)
        {
            var yearContainer = YearContainersRealization.SingleOrDefault(x => x.Year == year);
            if (yearContainer == null)
            {
                yearContainer = new ContainerYear(year, Accuracy / 100.0);
                YearContainersRealization.Add(yearContainer);
            }

            //создание контейнеров между годами (если они еще не созданы)
            var min = YearContainersRealization.Min(x => x.Year);
            var max = YearContainersRealization.Max(x => x.Year);
            for (int i = min; i <= max; i++)
            {
                if (YearContainersRealization.All(x => x.Year != i))
                    GetYearContainerRealization(i);
            }

            return yearContainer;
        }

        #endregion

        #endregion

        /// <summary>
        /// Обновление целевых сумм
        /// </summary>
        private void RefreshTargetSums()
        {
            if (!MonthContainersOit.Any() || MonthContainersOit.All(containerMonth => containerMonth.IsPast))
                return;

            var targetMonthContainers = MonthContainersOit.Where(containerMonth => !containerMonth.IsPast).ToList();
            var sum = 
                targetMonthContainers
                    .SelectMany(x => x.Items)
                    .Where(flatReportItem => flatReportItem.InReport && !flatReportItem.IsLoosen)
                    .Sum(flatReportItem => flatReportItem.Sum);

            //средняя сумма ОИТ в месяц
            targetMonthContainers.ForEach(x => x.TargetSum = sum / targetMonthContainers.Count);
        }

        /// <summary>
        /// Получить юнит с внедренными данными (эти юниты не сохранять!)
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        public SalesUnit GetSalesUnitWithInjectedData(SalesUnit salesUnit)
        {
            var reportItem = Items.Single(flatReportItem => flatReportItem.SalesUnits.ContainsById(salesUnit));

            //внедрение стоимости
            if (!Equals(reportItem.EstimatedCost, salesUnit.Cost))
            {
                salesUnit.Cost = reportItem.EstimatedCost;
            }

            //внедрение ПЗ
            if (!Equals(reportItem.EstimatedPrice, salesUnit.Price))
            {
                salesUnit.Price = reportItem.EstimatedPrice;
            }

            //внедрение даты ОИТ
            salesUnit.OrderInTakeDateInjected = reportItem.EstimatedOrderInTakeDate;
            if (!Equals(reportItem.OriginalOrderInTakeDate, reportItem.EstimatedOrderInTakeDate))
            {
                //salesUnit.OrderInTakeDateInjected = reportItem.EstimatedOrderInTakeDate;
                salesUnit.StartProductionDate = reportItem.EstimatedOrderInTakeDate;
            }

            //внедрение даты реализации
            if (!Equals(reportItem.OriginalRealizationDate, reportItem.EstimatedRealizationDate))
            {
                salesUnit.EndProductionDate = reportItem.EstimatedRealizationDate;
                salesUnit.ShipmentDate = reportItem.EstimatedRealizationDate;
                salesUnit.RealizationDate = reportItem.EstimatedRealizationDate;
                salesUnit.DeliveryDate = null;
            }

            return salesUnit;
        }

    }
}