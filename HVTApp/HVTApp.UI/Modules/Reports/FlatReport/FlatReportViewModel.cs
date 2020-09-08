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
        private readonly DateTime _startDateDefault = new DateTime(DateTime.Today.Year - 1, 1, 1);
        private readonly DateTime _finishDateDefault = DateTime.Today.AddYears(2);
        private DateTime _startDate;
        private DateTime _finishDate;
        private double _accuracy = 5;
        private FlatReportItem _selectedItem;

        #endregion

        #region Props

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value > FinishDate) return;
                _startDate = value;
                //����� ��� �������� �����������
                GetYearContainerOit(StartDate.Year);
                RefreshInReportStatus();
                RefreshContainers();
                OnPropertyChanged();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (value < StartDate) return;
                _finishDate = value;
                //����� ��� �������� �����������
                GetYearContainerOit(FinishDate.Year);
                RefreshInReportStatus();
                RefreshContainers();
                OnPropertyChanged();
            }
        }

        private void RefreshInReportStatus()
        {
            int monthsCount = MonthContainersOit.Count();
            var startOit = new DateTime(StartDate.Year, StartDate.Month, 1);
            var finishOit = new DateTime(FinishDate.Year, FinishDate.Month, DateTime.DaysInMonth(FinishDate.Year, FinishDate.Month));
            var monthContainersOit = YearContainersOit.SelectMany(x => x.MonthContainers).ToList();
            monthContainersOit.Where(x => !x.Date.BetweenDates(startOit, finishOit)).ForEach(x => x.InReport = false);
            monthContainersOit.Where(x => x.Date.BetweenDates(startOit, finishOit)).ForEach(x => x.InReport = true);

            Items.Where(x => !x.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate)).ForEach(x => x.InReport = false);

            if(monthsCount != MonthContainersOit.Count())
                RefreshTargetSums();

            RefreshInReportStatusRealization();
        }

        private void RefreshInReportStatusRealization()
        {
            if (Items.All(x => !x.InReport))
                return;

            var startRealization = Items.Where(x => x.InReport).Min(x => x.EstimatedRealizationDate);
            startRealization = new DateTime(startRealization.Year, startRealization.Month, 1);
            var finishRealization = Items.Where(x => x.InReport).Max(x => x.EstimatedRealizationDate);
            finishRealization = new DateTime(finishRealization.Year, finishRealization.Month, DateTime.DaysInMonth(finishRealization.Year, finishRealization.Month));
            var monthContainersRealization = YearContainersRealization.SelectMany(x => x.MonthContainers).ToList();
            monthContainersRealization.Where(x => !x.Date.BetweenDates(startRealization, finishRealization)).ForEach(x => x.InReport = false);
            monthContainersRealization.Where(x => x.Date.BetweenDates(startRealization, finishRealization)).ForEach(x => x.InReport = true);
        }

        /// <summary>
        /// �������� ������������ (� ���������)
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

        public ObservableCollection<ContainerYear> YearContainersOit { get; } = new ObservableCollection<ContainerYear>();

        public IEnumerable<ContainerMonth> MonthContainersOit => YearContainersOit.SelectMany(x => x.MonthContainers).Where(x => x.InReport).OrderBy(x => x.Year).ThenBy(x => x.Month);

        public List<FlatReportItemManagerContainer> ManagerContainersOit => Items
            .Where(x => x.InReport)
            .GroupBy(x => new {x.Manager, x.EstimatedOrderInTakeDate.Year})
            .Select(x => new FlatReportItemManagerContainer(x))
            .OrderBy(x => x.Manager)
            .ThenBy(x => x.Year).ToList();

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
        /// ������������� �������� ������
        /// </summary>
        public ICommand ReloadCommand { get; set; }

        /// <summary>
        /// ��������� ������
        /// </summary>
        public ICommand AlignCommand { get; }

        /// <summary>
        /// ������������ ����� ������
        /// </summary>
        public ICommand MakeSalesReportCommand { get; }

        /// <summary>
        /// ������������ ����� �����������
        /// </summary>
        public ICommand MakePaymentsReportCommand { get; }

        /// <summary>
        /// ��������� ����� � ���
        /// </summary>
        public ICommand AddMonthToOitCommand { get; }

        /// <summary>
        /// ��������� ����� � ����������
        /// </summary>
        public ICommand AddMonthToRealizationCommand { get; }

        /// <summary>
        /// ��������� ������ � ������
        /// </summary>
        public ICommand SaveBudgetCommand { get; }

        /// <summary>
        /// �������� � ��������
        /// </summary>
        public ICommand CompareBudgetCommand { get; }

        /// <summary>
        /// ��������� ������
        /// </summary>
        public ICommand LoadBudgetCommand { get; }

        #endregion
        
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

            ReloadCommand = new DelegateCommand(LoadDefault);

            CompareBudgetCommand = new DelegateCommand(
                () =>
                {
                    var budgets = UnitOfWork.Repository<Budget>().GetAll();
                    var budget = Container.Resolve<ISelectService>().SelectItem(budgets);
                    if (budget != null)
                    {
                        var viewModel = new BudgetComparisionViewModel(Items, budget.Units, Container);
                        Container.Resolve<IDialogService>().Show(viewModel, $"��������� � �������� �� {budget.Date.ToShortDateString()} {budget.Date.ToShortTimeString()}");
                    }
                });

            SaveBudgetCommand = new DelegateCommand(
                () =>
                {
                    var budget = new Budget
                    {
                        DateStart = StartDate,
                        DateFinish = FinishDate,
                        Date = DateTime.Now,
                        Name = $"����: {DateTime.Today.ToShortDateString()}, �����: {DateTime.Now.ToShortTimeString()}"
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
                    Container.Resolve<IMessageService>()
                        .ShowOkMessageDialog("����������", $"������ \"{budget}\" ������� ��������.");
                },
                () => new List<Role> {Role.Admin, Role.Director, Role.ReportMaker}.Contains(GlobalAppProperties.User.RoleCurrent));

            LoadBudgetCommand = new DelegateCommand(
                () =>
                {
                    var budgets = UnitOfWork.Repository<Budget>().GetAll();
                    var budget = Container.Resolve<ISelectService>().SelectItem(budgets);
                    if (budget != null)
                    {
                        var items = new List<FlatReportItem>();
                        var groups = budget.Units.GroupBy(x => new BudgetUnitComparer());
                        foreach (var grp in groups)
                        {
                            var salesUnits = grp.Select(x => x.SalesUnit);
                            var budgetUnit = grp.First();
                            items.Add(new FlatReportItem(salesUnits, !budgetUnit.IsRemoved, budgetUnit.Cost, budgetUnit.OrderInTakeDate, budgetUnit.RealizationDate, budgetUnit.PaymentConditionSet));
                        }

                        LoadBase(items, budget.DateStart, budget.DateFinish);
                    }
                });

            #region MakeReportCommand

            MakeSalesReportCommand = new DelegateCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var salesUnits = Items.Where(x => x.InReport).SelectMany(x => x.SalesUnits).ToList();
                    var salesUnits2 = unitOfWork.Repository<SalesUnit>().Find(x => salesUnits.ContainsById(x)).Select(GetSalesUnitWithInjactedData).ToList();
                    var salesReportViewModel = new SalesReportViewModel(Container, unitOfWork, salesUnits2);
                    Container.Resolve<IDialogService>().Show(salesReportViewModel, $"������. ������ ������������ ������: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
                });

            MakePaymentsReportCommand = new DelegateCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var salesUnits = Items.Where(x => x.InReport).Where(x => !x.IsLoosen).SelectMany(x => x.SalesUnits).ToList();
                    var salesUnits2 = unitOfWork.Repository<SalesUnit>().Find(x => salesUnits.ContainsById(x)).Select(GetSalesUnitWithInjactedData).ToList();
                    var paymentsPlanViewModel = new PaymentsPlanViewModel(Container, load:false);
                    paymentsPlanViewModel.Load(salesUnits2);

                    Container.Resolve<IDialogService>().Show(paymentsPlanViewModel, $"�����������. ������ ������������ ������: {DateTime.Today.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
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

            AlignCommand = new DelegateCommand(
                () =>
                {
                    var monthContainers = FlatReportComparator.Align(YearContainersOit.SelectMany(x => x.MonthContainers).Where(x => x.InReport)).ToList();
                    monthContainers.Cast<ContainerMonthOit>().ForEach(x => x.FillEstimatedDates());

                    if (monthContainers.Any(x => !x.IsOk))
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("����������", "�� �� ���� ������� ������� ��������� ����� � �������� ���������.");
                });

            LoadDefault();
        }

        private void LoadDefault()
        {
            //�������� ��������� ������
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            var items = _salesUnits
                .GroupBy(x => x, new SalesUnitsReportComparer())
                .Select(x => new FlatReportItem(x, x.Key.OrderInTakeDate.BetweenDates(_startDateDefault, _finishDateDefault)))
                .OrderBy(x => x.EstimatedOrderInTakeDate).ToList();
            
            //����������� ��� � ������� ��������
            items.Where(x => Math.Abs(x.SalesUnit.Cost) < 0.001).ForEach(x => x.EstimatedCost = GlobalAppProperties.PriceService.GetPrice(x.SalesUnit, x.EstimatedOrderInTakeDate).SumTotal * 1.4);

            LoadBase(items, _startDateDefault, _finishDateDefault);
        }

        private void LoadBase(List<FlatReportItem> items, DateTime startDate, DateTime finishDate)
        {
            _startDate = startDate;
            _finishDate = finishDate;
            Items.Clear();
            YearContainersOit.Clear();
            YearContainersRealization.Clear();

            //�������� �� ������� ��������� �������� ����������
            foreach (var item in items)
            {
                //����������/��������� ������ � �����
                item.InReportIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        //��������� ������ ������������� ������
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
                    RefreshContainers();
                };

                //��������� ������ ��� � ������
                item.EstimatedOrderInTakeMonthIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        RemoveItemFromContainerOit(item);

                        //�� ������� �� ����� �� ������?
                        if (!item.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate))
                        {
                            item.InReport = false;
                            Container.Resolve<IMessageService>().ShowOkMessageDialog("����������", $"\"{item}\" ����� �� ����� ��������� ��������� ��� (�� ���� ���).");
                        }
                        else
                        {
                            AddItemToContainerOit(item);
                        }

                        OnPropertyChanged(nameof(MonthContainersOit));
                    }
                };

                //��������� ������ ���������� � ������
                item.EstimatedRealizationMonthIsChanged += () =>
                {
                    if (item.InReport)
                    {
                        RemoveItemFromContainerRealization(item);
                        AddItemToContainerRealization(item);
                        RefreshInReportStatusRealization();
                        OnPropertyChanged(nameof(MonthContainersRealization));
                    }
                };

                //��������� ���� � ������
                item.EstimatedCostIsChanged += () =>
                {
                    RefreshTargetSums();
                    RefreshContainers();
                };
            }

            Items.AddRange(items);

            StartDate = startDate;
            FinishDate = finishDate;

            OnPropertyChanged(nameof(MonthContainersRealization));
        }

        private void RefreshContainers()
        {
            OnPropertyChanged(nameof(MonthContainersOit));
            OnPropertyChanged(nameof(MonthContainersRealization));
        }

        #region ItemAddRemove

        #region OrderInTake

        /// <summary>
        /// ���������� ������ �� ���������� ���
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerOit(FlatReportItem item)
        {
            YearContainersOit.SelectMany(x => x.MonthContainers).SingleOrDefault(x => x.Items.Contains(item))?.Items.Remove(item);
        }

        /// <summary>
        /// ���������� ������ � ��������� ���
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToContainerOit(FlatReportItem item)
        {
            //���� ����� �� �������� � �������� �������� ���
            if (!item.EstimatedOrderInTakeDate.BetweenDates(StartDate, FinishDate))
                return;

            GetMonthContainerOit(item.EstimatedOrderInTakeDate).Items.Add(item);
        }

        private ContainerMonth GetMonthContainerOit(DateTime date)
        {
            return GetYearContainerOit(date.Year).MonthContainers.Single(x => x.Month == date.Month);
        }

        private ContainerYear GetYearContainerOit(int year)
        {
            var yearContainer = YearContainersOit.SingleOrDefault(x => x.Year == year);
            if (yearContainer == null)
            {
                yearContainer = new ContainerYear(year, Accuracy / 100.0);
                YearContainersOit.Add(yearContainer);
            }

            //�������� ����������� ����� ������ (���� ��� ��� �� �������)
            var min = YearContainersOit.Min(x => x.Year);
            var max = YearContainersOit.Max(x => x.Year);
            for (int i = min; i <= max; i++)
            {
                if (YearContainersOit.All(x => x.Year != i))
                    GetYearContainerOit(i);
            }

            return yearContainer;
        }


        #endregion

        #region Realization

        /// <summary>
        /// ���������� ������ �� ���������� ����������
        /// </summary>
        /// <param name="item"></param>
        private void RemoveItemFromContainerRealization(FlatReportItem item)
        {
            YearContainersRealization.SelectMany(x => x.MonthContainers).SingleOrDefault(x => x.Items.Contains(item))?.Items.Remove(item);
        }

        /// <summary>
        /// ���������� ������ � ��������� ����������
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

            //�������� ����������� ����� ������ (���� ��� ��� �� �������)
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
        /// ���������� ������� ����
        /// </summary>
        private void RefreshTargetSums()
        {
            if (!MonthContainersOit.Any() || MonthContainersOit.All(x => x.IsPast))
                return;

            var targetMonthContainers = MonthContainersOit.Where(x => !x.IsPast).ToList();
            var sum = targetMonthContainers.SelectMany(x => x.Items).Where(x => x.InReport && !x.IsLoosen).Sum(x => x.Sum);

            //������� ����� ��� � �����
            targetMonthContainers.ForEach(x => x.TargetSum = sum / targetMonthContainers.Count);
        }

        /// <summary>
        /// �������� ���� � ����������� ������� (��� ����� �� ���������!)
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        public SalesUnit GetSalesUnitWithInjactedData(SalesUnit salesUnit)
        {
            var reportItem = Items.Single(x => x.SalesUnits.ContainsById(salesUnit));

            //��������� ���������
            if (!Equals(reportItem.EstimatedCost, salesUnit.Cost))
            {
                salesUnit.Cost = reportItem.EstimatedCost;
            }

            //��������� ���� ���
            if (!Equals(reportItem.OriginalOrderInTakeDate, reportItem.EstimatedOrderInTakeDate))
            {
                salesUnit.OrderInTakeDateInjected = reportItem.EstimatedOrderInTakeDate;
                salesUnit.StartProductionDate = reportItem.EstimatedOrderInTakeDate;
            }

            //��������� ���� ����������
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