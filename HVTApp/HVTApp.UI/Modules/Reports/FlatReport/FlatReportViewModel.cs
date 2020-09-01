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
        private List<FlatReportItem> _items;
        private DateTime _startDate;
        private DateTime _finishDate;
        private double _accuracy = 20;
        private FlatReportItem _selectedItem;
        private readonly ObservableCollection<FlatReportItem> _itemsFiltred = new ObservableCollection<FlatReportItem>();

        private readonly ObservableCollection<MonthContainerOit> _monthContainersOit = new ObservableCollection<MonthContainerOit>();
        private readonly ObservableCollection<FlatReportItemYearContainer> _yearContainersOit = new ObservableCollection<FlatReportItemYearContainer>();
        private readonly ObservableCollection<FlatReportItemManagerContainer> _managerContainersOit = new ObservableCollection<FlatReportItemManagerContainer>();

        private readonly ObservableCollection<MonthContainerRealization> _monthContainersRealization = new ObservableCollection<MonthContainerRealization>();
        private readonly ObservableCollection<FlatReportItemYearContainer> _yearContainersRealization = new ObservableCollection<FlatReportItemYearContainer>();
        private readonly ObservableCollection<FlatReportItemManagerContainer> _managerContainersRealization = new ObservableCollection<FlatReportItemManagerContainer>();


        #endregion

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if(Equals(_finishDate, value)) return;
                _finishDate = value;
                OnPropertyChanged(nameof(Items));
            }
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
                MonthContainersOit.ForEach(x => x.Accuracy = value / 100.0);
            }
        }

        public ObservableCollection<FlatReportItem> Items
        {
            get
            {
                var itemsFiltred = _items.Where(x => x.EstimatedOrderInTakeDate <= FinishDate && x.EstimatedOrderInTakeDate >= StartDate).ToList();
                if (itemsFiltred.MembersAreSame(_itemsFiltred))
                {
                    return _itemsFiltred;
                }

                _itemsFiltred.Clear();
                _itemsFiltred.AddRange(itemsFiltred.OrderBy(x => x.EstimatedOrderInTakeDate));

                return _itemsFiltred;
            }
        }

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

        public ObservableCollection<MonthContainerOit> MonthContainersOit
        {
            get
            {
                _monthContainersOit.Clear();

                if (!Items.Any())
                    return _monthContainersOit;

                var reportItems = Items.Where(x => x.InReport).ToList();
                var itemsInOit = reportItems.Where(x => x.SalesUnit.OrderIsTaken).ToList();
                var itemsToOit = reportItems.Except(itemsInOit).ToList();

                //средняя сумма ОИТ в месяц
                var sumPerMonth = itemsToOit.Sum(x => x.Sum) / (itemsToOit.Max(x => x.EstimatedOrderInTakeDate).MonthsBetween(itemsToOit.Min(x => x.EstimatedOrderInTakeDate)) + 1);

                var containers = reportItems
                    .GroupBy(x => new { x.EstimatedOrderInTakeDate.Year, x.EstimatedOrderInTakeDate.Month })
                    .Select(x => new MonthContainerOit(x, GetTargetOitSumPerMonth(x, sumPerMonth), Accuracy / 100.0))
                    .ToList();

                //создаем оставшиеся контейнеры
                var date = new DateTime(StartDate.Year, StartDate.Month, 1);
                while (date <= FinishDate)
                {
                    if (!containers.Any(x => x.Year == date.Year && x.Month == date.Month))
                    {
                        var targetContainerSum = DateTime.Today >= date ? 0 : sumPerMonth;
                        containers.Add(new MonthContainerOit(date, targetContainerSum, Accuracy / 100.0));
                    }
                    date = date.AddMonths(1);
                }

                _monthContainersOit.AddRange(containers.OrderBy(x => x.Year).ThenBy(x => x.Month));

                return _monthContainersOit;
            }
        }

        public ObservableCollection<FlatReportItemYearContainer> YearContainersOit
        {
            get
            {
                _yearContainersOit.Clear();
                _yearContainersOit.AddRange(
                    MonthContainersOit
                        .GroupBy(x => x.Year)
                        .Select(x => new FlatReportItemYearContainer(x))
                        .OrderBy(x => x.Year));
                return _yearContainersOit;
            }
        }

        public ObservableCollection<FlatReportItemManagerContainer> ManagerContainersOit
        {
            get
            {
                _managerContainersOit.Clear();
                _managerContainersOit.AddRange(
                    Items
                        .Where(x => x.InReport)
                        .GroupBy(x => new {x.Manager, x.EstimatedOrderInTakeDate.Year})
                        .Select(x => new FlatReportItemManagerContainer(x))
                        .OrderBy(x => x.Manager)
                        .ThenBy(x => x.Year));
                return _managerContainersOit;
            }
        }

        #endregion

        #region Realization

        public ObservableCollection<MonthContainerRealization> MonthContainersRealization
        {
            get
            {
                _monthContainersRealization.Clear();

                if (!Items.Any())
                    return _monthContainersRealization;

                var reportItems = Items.Where(x => x.InReport).ToList();
                var itemsRealized = reportItems.Where(x => x.SalesUnit.OrderIsRealized).ToList();
                var itemsNotRealized = reportItems.Except(itemsRealized).ToList();

                var finishDate = reportItems.Max(x => x.EstimatedRealizationDate);

                //средняя сумма в месяц
                var sumPerMonth = itemsNotRealized.Sum(x => x.Sum) / (finishDate.MonthsBetween(DateTime.Today) + 1);

                var containers = reportItems
                    .GroupBy(x => new { x.EstimatedRealizationDate.Year, x.EstimatedRealizationDate.Month })
                    .Select(x => new MonthContainerRealization(x, GetTargetRealizationSumPerMonth(x, sumPerMonth), Accuracy / 100.0))
                    .ToList();

                //создаем оставшиеся контейнеры
                var date = new DateTime(StartDate.Year, StartDate.Month, 1);
                while (date <= finishDate)
                {
                    if (!containers.Any(x => x.Year == date.Year && x.Month == date.Month))
                    {
                        var targetContainerSum = DateTime.Today >= date ? 0 : sumPerMonth;
                        containers.Add(new MonthContainerRealization(date, targetContainerSum, Accuracy / 100.0));
                    }
                    date = date.AddMonths(1);
                }

                _monthContainersRealization.AddRange(containers.OrderBy(x => x.Year).ThenBy(x => x.Month));

                return _monthContainersRealization;
            }
        }

        public ObservableCollection<FlatReportItemYearContainer> YearContainersRealization
        {
            get
            {
                _yearContainersRealization.Clear();
                _yearContainersRealization.AddRange(
                    MonthContainersRealization
                        .GroupBy(x => x.Year)
                        .Select(x => new FlatReportItemYearContainer(x))
                        .OrderBy(x => x.Year));
                return _yearContainersRealization;
            }
        }

        public ObservableCollection<FlatReportItemManagerContainer> ManagerContainersRealization
        {
            get
            {
                _managerContainersRealization.Clear();
                _managerContainersRealization.AddRange(
                    Items
                        .Where(x => x.InReport)
                        .GroupBy(x => new { x.Manager, x.EstimatedRealizationDate.Year })
                        .Select(x => new FlatReportItemManagerContainer(x))
                        .OrderBy(x => x.Manager)
                        .ThenBy(x => x.Year));
                return _managerContainersRealization;
            }
        }

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
                    var items = Items.Where(x => x.InReport).ToList();
                    items.ForEach(x => x.InjectDates());

                    var paymentsPlanViewModel = new PaymentsPlanViewModel(Container, load:false);
                    paymentsPlanViewModel.Load(items.SelectMany(x => x.SalesUnits));

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
                    var diffList = new List<double>();


                    while (MonthContainersOit.Any(x => !x.IsOk))
                    {
                        var notOkContainers = MonthContainersOit.ToList();

                        var toHighContainers = notOkContainers.Where(x => x.Difference < 0).OrderBy(x => x.Difference).ToList();
                        foreach (var toHighContainer in toHighContainers)
                        {
                            //высокий контейнер (с него нужно скинуть в соседний)
                            if (toHighContainer.FlatReportItems.Any())
                            {
                                //поиск соседа, в который можно скинуть
                                var targetNeighboringContainer = GetNeighboringContainers(toHighContainer).OrderBy(x => x.Difference).LastOrDefault();
                                if (targetNeighboringContainer != null)
                                {
                                    var item = GetNearestItem(toHighContainer, targetNeighboringContainer, true);

                                    var dif1 = toHighContainer.Difference + item.Sum;
                                    var dif2 = targetNeighboringContainer.Difference - item.Sum;
                                    if (true)
                                    {
                                        toHighContainer.FlatReportItems.Remove(item);
                                        targetNeighboringContainer.FlatReportItems.Add(item);
                                    }
                                }
                            }
                        }

                        var toLowContainers = notOkContainers.Where(x => x.Difference > 0).OrderByDescending(x => x.Difference).ToList();
                        foreach (var toLowContainer in toLowContainers)
                        {
                            //высокий контейнер (с него нужно скинуть в соседний)
                            var targetNeighboringContainer = GetNeighboringContainers(toLowContainer).OrderBy(x => x.Difference).FirstOrDefault();
                            if (targetNeighboringContainer != null && targetNeighboringContainer.FlatReportItems.Any())
                            {
                                var item = GetNearestItem(targetNeighboringContainer, toLowContainer, false);

                                var dif1 = toLowContainer.Difference - item.Sum;
                                var dif2 = targetNeighboringContainer.Difference + item.Sum;
                                if (true)
                                {
                                    targetNeighboringContainer.FlatReportItems.Remove(item);
                                    toLowContainer.FlatReportItems.Add(item);
                                }

                            }
                        }

                        //notOkContainers.Remove(toHighContainer);

                        ////низкий контейнер
                        //var toLowContainer = notOkContainers.Where(x => x.Difference < 0).OrderBy(x => x.Difference).FirstOrDefault();
                        //if (toLowContainer != null)
                        //{
                        //    //поиск соседа-донора
                        //    var targetNeighboringContainer = GetNeighboringContainers(toLowContainer).OrderBy(x => x.Difference).LastOrDefault();
                        //    if (targetNeighboringContainer != null && targetNeighboringContainer.FlatReportItems.Any())
                        //    {
                        //        var item = GetNearestItam(targetNeighboringContainer, toLowContainer);
                        //        targetNeighboringContainer.FlatReportItems.Remove(item);
                        //        toLowContainer.FlatReportItems.Add(item);
                        //    }
                        //}

                        //выход из мертвого цикла
                        var dif = MonthContainersOit.Sum(x => x.Difference);
                        diffList.Add(dif);
                        if (diffList.Count(x => Math.Abs(x - dif) < 0.001) > 100)
                        {
                            break;
                        }

                    }

                    ////var containers = FlatReportComparator.Align(GenerateMonthContainers()).ToList();
                    //var containers = FlatReportComparator.Align(MonthContainers).ToList();
                    //containers.ForEach(x => x.FillEstimatedOrderInTakeDates());
                    //MonthContainers.Clear();
                    //MonthContainers.AddRange(containers);


                    var co = MonthContainersOit.ToList();
                    co.ForEach(x => x.FillEstimatedOrderInTakeDates());

                    MonthContainersOit.Clear();
                    MonthContainersOit.AddRange(co);

                    if (MonthContainersOit.Any(x => !x.IsOk))
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Не во всех месяцах удалось выровнять суммы с заданной точностью.");
                });

            Load();
        }

        private FlatReportItem GetNearestItem(FlatReportItemMonthContainer containerFrom, FlatReportItemMonthContainer containerTo, bool from = true)
        {
            var targetContainer = from ? containerFrom : containerTo;
            return containerFrom.FlatReportItems
                .Where(x => !x.SalesUnit.OrderIsTaken)
                .OrderBy(x => MonthsBetween(containerTo, x))
                .ThenBy(x => Math.Abs(Math.Abs(targetContainer.Difference) - x.Sum))
                .First();
        }

        /// <summary>
        /// Вернуть соседние контейнеры
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private IEnumerable<FlatReportItemMonthContainer> GetNeighboringContainers(FlatReportItemMonthContainer container)
        {
            var leftContainer = GetNeighboringContainer(container, -1);
            if (leftContainer != null && !leftContainer.IsPast)
                yield return leftContainer;

            var rightContainer = GetNeighboringContainer(container, 1);
            if (rightContainer != null && !rightContainer.IsPast)
                yield return rightContainer;
        }

        private FlatReportItemMonthContainer GetNeighboringContainer(FlatReportItemMonthContainer container, int monthCount)
        {
            var naighborDate = container.Date.AddMonths(monthCount);
            return MonthContainersOit.SingleOrDefault(x => x.Year == naighborDate.Year && x.Month == naighborDate.Month);
        }

        private int MonthsBetween(FlatReportItemMonthContainer container, FlatReportItem item)
        {
            var date = new DateTime(container.Year, container.Month, 1);
            return Math.Abs(item.OriginalOrderInTakeDate.MonthsBetween(date));
        }


        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            //загрузка продажных единиц
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport);

            _items = _salesUnits.GroupBy(x => x, new SalesUnitsReportComparer()).Select(x => new FlatReportItem(x)).ToList();
            
            //расстановка цен в нулевых единицах
            _items.Where(x => Math.Abs(x.SalesUnit.Cost) < 0.001).ForEach(x => x.EstimatedCost = GlobalAppProperties.PriceService.GetPrice(x.SalesUnit, x.EstimatedOrderInTakeDate).SumTotal * 1.4);
            
            //подписка на событие изменения ключевых параметров
            _items.ForEach(flatReportItem => flatReportItem.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FlatReportItem.InReport) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedOrderInTakeDate))
                {
                    if (flatReportItem.InReport)
                    {
                        if (flatReportItem.EstimatedOrderInTakeDate > FinishDate)
                        {
                            FinishDate = flatReportItem.EstimatedOrderInTakeDate;
                            OnPropertyChanged(nameof(FinishDate));
                        }                        
                    }
                }

                if (args.PropertyName == nameof(FlatReportItem.InReport) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedOrderInTakeDate) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedCost))
                {
                    OnPropertyChanged(nameof(MonthContainersOit));
                    OnPropertyChanged(nameof(YearContainersOit));
                    OnPropertyChanged(nameof(ManagerContainersOit));
                }

                if (args.PropertyName == nameof(FlatReportItem.InReport) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedRealizationDate) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedCost))
                {
                    OnPropertyChanged(nameof(MonthContainersRealization));
                    OnPropertyChanged(nameof(YearContainersRealization));
                    OnPropertyChanged(nameof(ManagerContainersRealization));
                }
            });

            _startDate = _items.Where(x => x.InReport).Min(x => x.EstimatedOrderInTakeDate);
            _finishDate = _items.Where(x => x.InReport).Max(x => x.EstimatedOrderInTakeDate);
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(FinishDate));
        }

        private double GetTargetOitSumPerMonth(IEnumerable<FlatReportItem> flatReportItems, double sum)
        {
            var itemsInOit = flatReportItems.Where(x => x.SalesUnit.OrderIsTaken).ToList();
            return flatReportItems.Count() == itemsInOit.Count ? flatReportItems.Sum(x => x.Sum) : sum;
        }

        private double GetTargetRealizationSumPerMonth(IEnumerable<FlatReportItem> flatReportItems, double sum)
        {
            var itemsRealizad = flatReportItems.Where(x => x.SalesUnit.OrderIsRealized).ToList();
            return flatReportItems.Count() == itemsRealizad.Count ? flatReportItems.Sum(x => x.Sum) : sum;
        }
    }
}