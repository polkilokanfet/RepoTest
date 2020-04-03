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
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Modules.Reports.ViewModels;
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

        public ObservableCollection<FlatReportItemMonthContainer> Containers { get; } = new ObservableCollection<FlatReportItemMonthContainer>();

        /// <summary>
        /// —уммарна€ цена на все отмеченные айтемы
        /// </summary>
        public double Sum => Items.Where(x => x.InReport).Sum(x => x.Sum);

        public ICommand ReloadCommand { get; set; }
        public ICommand AlignCommand { get; }
        public ICommand MakeReportCommand { get; }

        public ICommand AddMonthCommand { get; set; }

        public FlatReportViewModel(IUnityContainer container) : base(container)
        {
            this.PaymentsPlanViewModel = new PaymentsPlanViewModel(container, false);

            ReloadCommand = new DelegateCommand(Load);
            MakeReportCommand = new DelegateCommand(MakeReportExecuteMethod);

            AddMonthCommand = new DelegateCommand<string>(
                parameter =>
                {
                    int monthsAmount;
                    int.TryParse(parameter, out monthsAmount);
                    SelectedItem.EstimatedOrderInTakeDate = SelectedItem.EstimatedOrderInTakeDate.AddMonths(monthsAmount);
                },
                parameter => SelectedItem != null);

            AlignCommand = new DelegateCommand(
                () =>
                {
                    var containers = FlatReportComparator.Align(GenerateContainers()).ToList();
                    containers.ForEach(x => x.FillEstimatedOrderInTakeDates());
                    Containers.Clear();
                    Containers.AddRange(containers);

                    if(Containers.Any(x => !x.IsOk))
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("»нформаци€", "Ќе во всех мес€цах удалось выровн€ть суммы с заданной точностью.");
                });

            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && x.Project.ForReport);

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
            Containers.Clear();
            Containers.AddRange(GenerateContainers());
        }

        private IEnumerable<FlatReportItemMonthContainer> GenerateContainers()
        {
            if(!Items.Any())
                return new List<FlatReportItemMonthContainer>();

            var reportItems = Items.Where(x => x.InReport).ToList();
            var itemsInOit = reportItems.Where(x => x.SalesUnit.OrderIsTaken).ToList();
            var itemsToOit = reportItems.Except(itemsInOit).ToList();

            //средн€€ сумма ќ»“ в мес€ц
            var sumPerMonth = itemsToOit.Sum(x => x.Sum) / (itemsToOit.Max(x => x.EstimatedOrderInTakeDate).MonthsBetween(itemsToOit.Min(x => x.EstimatedOrderInTakeDate)) + 1);

            var containers = reportItems
                .GroupBy(x => new { x.EstimatedOrderInTakeDate.Year, x.EstimatedOrderInTakeDate.Month })
                .Select(x => new FlatReportItemMonthContainer(x, GetSumPerMonth(x, sumPerMonth), Accuracy / 100.0))
                .ToList();

            //создаем оставшиес€ контейнеры
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
    }
}