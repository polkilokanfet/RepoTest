using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.Reports.FlatReport.Comparator;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;
using HVTApp.UI.Modules.Reports.FlatReport.Reports;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public class FlatReportViewModel : ViewModelBaseCanExportToExcel
    {
        #region Fields

        private List<SalesUnit> _salesUnits;
        private DateTime _startDate = DateTime.Today.AddYears(-1);
        private DateTime _finishDate = DateTime.Today.AddYears(2);
        private double _accuracy = 20;
        private FlatReportItem _selectedItem;
        private readonly List<MonthContainerRealization> _monthContainersRealization = new List<MonthContainerRealization>();
        private double _currentSumPerMonthOit;
        private double _currentSumPerMonthRealization = 0;

        #endregion

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RefreshTargetSums();
                RefreshContainers();
                RefreshInReportStatus();
                OnPropertyChanged(nameof(MonthContainersRealization));
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                _finishDate = value;
                RefreshTargetSums();
                RefreshContainers();
                RefreshInReportStatus();
                OnPropertyChanged(nameof(MonthContainersRealization));
            }
        }

        private void RefreshInReportStatus()
        {
            Items.Where(x => x.EstimatedOrderInTakeDate < StartDate).ForEach(x => x.InReport = false);
            Items.Where(x => x.EstimatedOrderInTakeDate > FinishDate).ForEach(x => x.InReport = false);

            YearContainersOit.ForEach(x => x.InReport = false);
            YearContainersOit.Where(x => x.Year >= StartDate.Year && x.Year <= FinishDate.Year).ForEach(x => x.InReport = true);

            var start = new DateTime(StartDate.Year, StartDate.Month, 1);
            var finish = new DateTime(FinishDate.Year, FinishDate.Month, DateTime.DaysInMonth(FinishDate.Year, FinishDate.Month));
            var monthContainers = YearContainersOit.SelectMany(x => x.MonthContainers).ToList();
            monthContainers.ForEach(x => x.InReport = false);
            monthContainers.Where(x => x.Date >= start && x.Date <= finish).ForEach(x => x.InReport = true);
        }

        /// <summary>
        /// Точность выравнивания (в процентах)
        /// </summary>
        public double Accuracy
        {
            get { return _accuracy; }
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
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand<string>)AddMonthToOitCommand).RaiseCanExecuteChanged();
                ((DelegateCommand<string>)AddMonthToRealizationCommand).RaiseCanExecuteChanged();
            }
        }

        public object[] SelectedItems { get; set; }

        #region OrderInTake

        public ObservableCollection<FlatReportItemYearContainer> YearContainersOit { get; } = new ObservableCollection<FlatReportItemYearContainer>();

        public IEnumerable<FlatReportItemMonthContainer> MonthContainersOit => YearContainersOit.SelectMany(x => x.MonthContainers);

        public List<FlatReportItemManagerContainer> ManagerContainersOit => Items
            .Where(x => x.InReport)
            .GroupBy(x => new {x.Manager, x.EstimatedOrderInTakeDate.Year})
            .Select(x => new FlatReportItemManagerContainer(x))
            .OrderBy(x => x.Manager)
            .ThenBy(x => x.Year).ToList();

        #endregion

        #region Realization

        public List<MonthContainerRealization> MonthContainersRealization
        {
            get
            {
                var startDate = Items.Min(x => x.EstimatedRealizationDate);
                var start = new DateTime(startDate.Year, startDate.Month, 1);
                var finishDate = Items.Max(x => x.EstimatedRealizationDate);
                var finish = new DateTime(finishDate.Year, finishDate.Month, DateTime.DaysInMonth(finishDate.Year, finishDate.Month));
                return _monthContainersRealization.Where(x => x.Date >= start && x.Date <= finish).OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();
            }
        }

        public List<FlatReportItemYearContainer> YearContainersRealization => MonthContainersRealization
            .GroupBy(x => x.Year)
            .Select(x => new FlatReportItemYearContainer(x.Key))
            .OrderBy(x => x.Year).ToList();

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
        public ICommand ReloadCommand { get; set; }

        /// <summary>
        /// Выровнять данные
        /// </summary>
        public ICommand AlignCommand { get; }

        /// <summary>
        /// Свормировать отчет Бюджет
        /// </summary>
        public ICommand MakeSalesReportCommand { get; }

        /// <summary>
        /// Свормировать отчет Поступления
        /// </summary>
        public ICommand MakePaymentsReportCommand { get; }

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
        public ICommand SaveBudgetCommand { get; set; }

        /// <summary>
        /// Сравнить с бюджетом
        /// </summary>
        public ICommand CompareBudgetCommand { get; set; }

        #endregion

        public FlatReportViewModel(IUnityContainer container) : base(container)
        {
            Items.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (var item in args.NewItems.Cast<FlatReportItem>())
                    {
                        if(!item.InReport) continue;
                        AddItemToContainerOit(item);
                        AddItemToContainerRealization(item);
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (var item in args.OldItems.Cast<FlatReportItem>())
                    {
                        RemoveItemFromContainerOit(item);
                        RemoveItemFromContainerRealization(item);
                    }
                }
            };

            CompareBudgetCommand = new DelegateCommand(
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

            SaveBudgetCommand = new DelegateCommand(
                () =>
                {
                    var budget = new Budget
                    {
                        Name = $"Дата: {DateTime.Today.ToShortDateString()}, время: {DateTime.Now.ToShortTimeString()}"
                    };

                    foreach (var flatReportItem in Items)
                    {
                        foreach (var salesUnit in flatReportItem.SalesUnits)
                        {
                            var budgetUnit = new BudgetUnit
                            {
                                Budget = budget,
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
                            
                            salesUnit.BudgetUnits.Add(budgetUnit);
                            budget.Units.Add(budgetUnit);
                        }
                    }

                    UnitOfWork.SaveChanges();
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", $"Бюджет \"{budget}\" успешно сохранен.");
                });

            ReloadCommand = new DelegateCommand(Load);

            MakeSalesReportCommand = new DelegateCommand(
                () =>
                {
                    var salesReportViewModel = new SalesReportViewModel(Container, Items.Where(x => x.InReport).ToList());
                    Container.Resolve<IDialogService>().Show(salesReportViewModel, $"Бюджет. Момент формирования отчета: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                });

            MakePaymentsReportCommand = new DelegateCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var paymentsPlanViewModel = new PaymentsPlanViewModel(Container, load:false);
                    paymentsPlanViewModel.Load(Items.Where(x => x.InReport).SelectMany(x => x.GetSalesUnitsWithInjactedData(unitOfWork)));

                    Container.Resolve<IDialogService>().Show(paymentsPlanViewModel, $"Поступления. Момент формирования отчета: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                });

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

            AlignCommand = new DelegateCommand(
                () =>
                {
                    var monthContainers = FlatReportComparator.Align(YearContainersOit.SelectMany(x => x.MonthContainers).Where(x => x.InReport)).ToList();
                    monthContainers.Cast<MonthContainerOit>().ForEach(x => x.FillEstimatedOrderInTakeDates());

                    if (monthContainers.Any(x => !x.IsOk))
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Не во всех месяцах удалось выровнять суммы с заданной точностью.");
                });

            Load();
        }

        private void Load()
        {
            Items.Clear();
            YearContainersOit.Clear();
            _monthContainersRealization.Clear();

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            //загрузка продажных единиц
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            var items = _salesUnits
                .GroupBy(x => x, new SalesUnitsReportComparer())
                .Select(x => new FlatReportItem(x, x.Key.OrderInTakeDate.BetweenDates(StartDate, FinishDate)))
                .OrderBy(x => x.EstimatedOrderInTakeDate).ToList();
            
            //расстановка цен в нулевых единицах
            items.Where(x => Math.Abs(x.SalesUnit.Cost) < 0.001).ForEach(x => x.EstimatedCost = GlobalAppProperties.PriceService.GetPrice(x.SalesUnit, x.EstimatedOrderInTakeDate).SumTotal * 1.4);
            
            //подписка на событие изменения ключевых параметров
            foreach (var item in items)
            {
                //исключение/включение айтема в отчет
                item.InReportIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        AddItemToContainerOit(item);
                        AddItemToContainerRealization(item);
                    }
                    else
                    {
                        RemoveItemFromContainerOit(item);
                        RemoveItemFromContainerRealization(item);
                    }

                    RefreshTargetSums();
                    RefreshContainers();
                };

                //изменение месяца ОИТ в айтеме
                item.EstimatedOrderInTakeMonthIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        RemoveItemFromContainerOit(item);
                        AddItemToContainerOit(item);

                        RefreshTargetSums();
                        RefreshContainers();
                    }
                };

                //изменение месяца реализации в айтеме
                item.EstimatedRealizationMonthIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        RemoveItemFromContainerRealization(item);
                        AddItemToContainerRealization(item);

                        RefreshContainers();
                    }
                };

                //изменение в айтеме
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(FlatReportItem.EstimatedCost))
                    {
                        RefreshTargetSums();
                        RefreshContainers();
                    }
                };
            }

            Items.AddRange(items);

            StartDate = DateTime.Today.AddYears(-1);
            FinishDate = DateTime.Today.AddYears(2);

            OnPropertyChanged(nameof(MonthContainersRealization));
        }

        private void RefreshContainers()
        {
            OnPropertyChanged(nameof(MonthContainersOit));

            OnPropertyChanged(nameof(YearContainersOit));
            OnPropertyChanged(nameof(ManagerContainersOit));

            OnPropertyChanged(nameof(YearContainersRealization));
            OnPropertyChanged(nameof(ManagerContainersRealization));
        }

        #region ItemAddRemove

        /// <summary>
        /// Исключение айтема из контейнера ОИТ
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerOit(FlatReportItem item)
        {
            YearContainersOit.SelectMany(x => x.MonthContainers).SingleOrDefault(x => x.FlatReportItems.Contains(item))?.FlatReportItems.Remove(item);
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

            GetMonthContainer(item.EstimatedOrderInTakeDate).FlatReportItems.Add(item);
        }

        private FlatReportItemMonthContainer GetMonthContainer(DateTime date)
        {
            return GetYearContainer(date.Year).MonthContainers.Single(x => x.Month == date.Month);
        }

        private FlatReportItemYearContainer GetYearContainer(int year)
        {
            var yearContainer = YearContainersOit.SingleOrDefault(x => x.Year == year);
            if (yearContainer == null)
            {
                yearContainer = new FlatReportItemYearContainer(year);
                YearContainersOit.Add(yearContainer);
            }
            return yearContainer;
        }

        /// <summary>
        /// Исключение айтема из контейнера Реализации
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerRealization(FlatReportItem item)
        {
            _monthContainersRealization.SingleOrDefault(x => x.FlatReportItems.Contains(item))?.FlatReportItems.Remove(item);
        }

        /// <summary>
        /// Добавление айтема в контейнер Реализации
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToContainerRealization(FlatReportItem item)
        {
            var container = _monthContainersRealization.SingleOrDefault(x => x.Year == item.EstimatedRealizationDate.Year && x.Month == item.EstimatedRealizationDate.Month);
            if (container == null)
            {
                container = new MonthContainerRealization(new[] { item }, Accuracy / 100.0);
                _monthContainersRealization.Add(container);
            }
            else
            {
                container.FlatReportItems.Add(item);
            }
        }
        

        #endregion

        private void RefreshTargetSums()
        {
            //var itemsToOit = Items.Where(x => x.InReport).Where(x => !x.IsLoosen).Where(x => !x.SalesUnit.OrderIsTaken).ToList();

            ////средняя сумма ОИТ в месяц
            //var sumPerMonth = itemsToOit.Any()
            //    ? itemsToOit.Sum(x => x.Sum) / (itemsToOit.Max(x => x.EstimatedOrderInTakeDate).MonthsBetween(itemsToOit.Min(x => x.EstimatedOrderInTakeDate)) + 1)
            //    : 0;

            //if (Math.Abs(_currentSumPerMonthOit - sumPerMonth) < 0.001)
            //    return;

            //foreach (var monthContainerOit in MonthContainersOit)
            //{
            //    monthContainerOit.TargetSum = monthContainerOit.IsPast 
            //        ? monthContainerOit.CurrentSum
            //        : sumPerMonth;
            //}

            //_currentSumPerMonthOit = sumPerMonth;
        }
    }
}