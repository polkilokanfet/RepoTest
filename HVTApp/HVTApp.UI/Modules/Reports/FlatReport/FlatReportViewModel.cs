using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    public partial class FlatReportViewModel : ViewModelBaseCanExportToExcel
    {
        private List<SalesUnit> _salesUnits;
        private List<FlatReportItem> _items;
        private DateTime _startDate;
        private DateTime _finishDate;
        private double _accuracy = 20;
        private FlatReportItem _selectedItem;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                OnPropertyChanged();
                RefreshItems();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if(Equals(_finishDate, value)) return;
                _finishDate = value;
                OnPropertyChanged();
                RefreshItems();
            }
        }

        public double Accuracy
        {
            get { return _accuracy; }
            set
            {
                if (Equals(_accuracy, value)) return;
                if(value < 0) return;
                _accuracy = value;
            }
        }

        public ObservableCollection<FlatReportItem> Items { get; } = new ObservableCollection<FlatReportItem>();

        public FlatReportItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand<string>)AddMonthCommand).RaiseCanExecuteChanged();
            }
        }

        public object[] SelectedItems { get; set; }

        public ObservableCollection<FlatReportItemMonthContainer> MonthContainers { get; } = new ObservableCollection<FlatReportItemMonthContainer>();
        public ObservableCollection<FlatReportItemYearContainer> YearContainers { get; } = new ObservableCollection<FlatReportItemYearContainer>();
        public ObservableCollection<FlatReportItemManagerContainer> ManagerContainers { get; } = new ObservableCollection<FlatReportItemManagerContainer>();

        /// <summary>
        /// Суммарная цена на все отмеченные айтемы
        /// </summary>
        public double Sum => Items.Where(x => x.InReport).Sum(x => x.Sum);

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
        /// Свормировать отчеты
        /// </summary>
        public ICommand MakeReportCommand { get; }

        /// <summary>
        /// Прибавить месяц
        /// </summary>
        public ICommand AddMonthCommand { get; set; }

        /// <summary>
        /// Сохранить данные в бюджет
        /// </summary>
        public ICommand SaveBudgetCommand { get; set; }

        #endregion

        public FlatReportViewModel(IUnityContainer container) : base(container)
        {
            this.PaymentsPlanViewModel = new PaymentsPlanViewModel(container, false);

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
                                Cost = flatReportItem.Cost,
                                CostByManager = salesUnit.Cost,
                                OrderInTakeDate = flatReportItem.EstimatedOrderInTakeDate,
                                OrderInTakeDateByManager = salesUnit.OrderInTakeDate,
                                RealizationDate = flatReportItem.RealizationDate,
                                RealizationDateByManager = salesUnit.RealizationDateCalculated,
                                PaymentConditionSet = flatReportItem.PaymentConditionSet,
                                PaymentConditionSetByManager = salesUnit.PaymentConditionSet
                            };
                            
                            salesUnit.BudgetUnits.Add(budgetUnit);
                            budget.Units.Add(budgetUnit);
                        }
                    }

                    UnitOfWork.SaveChanges();
                    Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Бюджет успешно сохранен.");
                });

            ReloadCommand = new DelegateCommand(Load);
            MakeReportCommand = new DelegateCommand(MakeReportExecuteMethod);

            AddMonthCommand = new DelegateCommand<string>(
                parameter =>
                {
                    int monthsAmount;
                    int.TryParse(parameter, out monthsAmount);
                    var selectedItems = SelectedItems.Cast<FlatReportItem>();
                    selectedItems.ForEach(x => x.EstimatedOrderInTakeDate = x.EstimatedOrderInTakeDate.AddMonths(monthsAmount));
                },
                parameter => SelectedItem != null);

            AlignCommand = new DelegateCommand(
                () =>
                {
                    var diffList = new List<double>();


                    while (MonthContainers.Any(x => !x.IsOk))
                    {
                        var notOkContainers = MonthContainers.ToList();

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
                        var dif = MonthContainers.Sum(x => x.Difference);
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


                    var co = MonthContainers.ToList();
                    co.ForEach(x => x.FillEstimatedOrderInTakeDates());

                    MonthContainers.Clear();
                    MonthContainers.AddRange(co);

                    if (MonthContainers.Any(x => !x.IsOk))
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
            return MonthContainers.SingleOrDefault(x => x.Year == naighborDate.Year && x.Month == naighborDate.Month);
        }

        private int MonthsBetween(FlatReportItemMonthContainer container, FlatReportItem item)
        {
            var date = new DateTime(container.Year, container.Month, 1);
            return Math.Abs(item.OriginalOrderInTakeDate.MonthsBetween(date));
        }


        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport);

            _salesUnits.Where(x => Math.Abs(x.Cost) < 0.00001).ForEach(x =>
            {
                x.Cost = GlobalAppProperties.PriceService.GetPrice(x, x.OrderInTakeDate).SumTotal * 1.4;
            });

            _startDate = _salesUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _salesUnits.Max(x => x.OrderInTakeDate);
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(FinishDate));

            _items = _salesUnits
                .GroupBy(x => x, new SalesUnitsReportComparer())
                .Select(x => new FlatReportItem(x)).ToList();

            _items.ForEach(x => x.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FlatReportItem.InReport) ||
                    args.PropertyName == nameof(FlatReportItem.EstimatedOrderInTakeDate))
                {
                    RefreshContainers();
                }
            });

            RefreshItems();
            SalesReportUnits.Clear();
        }

        private void RefreshItems()
        {
            var items = _items
                .Where(x => x.EstimatedOrderInTakeDate <= FinishDate && x.EstimatedOrderInTakeDate >= StartDate)
                .OrderBy(x => x.EstimatedOrderInTakeDate);

            Items.Clear();
            Items.AddRange(items);

            OnPropertyChanged(nameof(Sum));

            RefreshContainers();
        }

        private void RefreshContainers()
        {
            MonthContainers.Clear();
            MonthContainers.AddRange(GenerateMonthContainers());

            YearContainers.Clear();
            YearContainers.AddRange(GenerateYearContainers());

            ManagerContainers.Clear();
            ManagerContainers.AddRange(GenerateManagerContainers());
        }

        private IEnumerable<FlatReportItemMonthContainer> GenerateMonthContainers()
        {
            if (!Items.Any())
                return new List<FlatReportItemMonthContainer>();

            var reportItems = Items.Where(x => x.InReport).ToList();
            var itemsInOit = reportItems.Where(x => x.SalesUnit.OrderIsTaken).ToList();
            var itemsToOit = reportItems.Except(itemsInOit).ToList();

            //средняя сумма ОИТ в месяц
            var sumPerMonth = itemsToOit.Sum(x => x.Sum) / (itemsToOit.Max(x => x.EstimatedOrderInTakeDate).MonthsBetween(itemsToOit.Min(x => x.EstimatedOrderInTakeDate)) + 1);

            var containers = reportItems
                .GroupBy(x => new { x.EstimatedOrderInTakeDate.Year, x.EstimatedOrderInTakeDate.Month })
                .Select(x => new FlatReportItemMonthContainer(x, GetSumPerMonth(x, sumPerMonth), Accuracy / 100.0))
                .ToList();

            //создаем оставшиеся контейнеры
            var date = new DateTime(StartDate.Year, StartDate.Month, 1);
            while (date <= FinishDate)
            {
                if (!containers.Any(x => x.Year == date.Year && x.Month == date.Month))
                {
                    var targetContainerSum = DateTime.Today >= date ? 0 : sumPerMonth;
                    containers.Add(new FlatReportItemMonthContainer(null, targetContainerSum, Accuracy / 100.0, date));
                }
                date = date.AddMonths(1);
            }

            return containers.OrderBy(x => x.Year).ThenBy(x => x.Month);
        }

        private double GetSumPerMonth(IEnumerable<FlatReportItem> flatReportItems, double sum)
        {
            var itemsInOit = flatReportItems.Where(x => x.SalesUnit.OrderIsTaken).ToList();
            return flatReportItems.Count() == itemsInOit.Count ? flatReportItems.Sum(x => x.Sum) : sum;
        }

        private IEnumerable<FlatReportItemYearContainer> GenerateYearContainers()
        {
            return MonthContainers
                .GroupBy(x => x.Year)
                .Select(x => new FlatReportItemYearContainer(x))
                .OrderBy(x => x.Year);
        }

        private IEnumerable<FlatReportItemManagerContainer> GenerateManagerContainers()
        {
            return Items
                .Where(x => x.InReport)
                .GroupBy(x => new { x.Manager, x.EstimatedOrderInTakeDate.Year })
                .Select(x => new FlatReportItemManagerContainer(x))
                .OrderBy(x => x.Manager)
                .ThenBy(x => x.Year);
        }
    }
}