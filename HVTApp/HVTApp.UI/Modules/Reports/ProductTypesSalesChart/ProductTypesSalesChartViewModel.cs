using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.ProductTypesSalesChart
{
    public class ProductTypesSalesChartViewModel : ViewModelBase
    {
        private List<SalesUnit> _salesUnits;
        private DateTime _startDate;
        private DateTime _finishDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                RefreshItems();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (Equals(_finishDate, value)) return;
                _finishDate = value;
                RefreshItems();
            }
        }

        public ObservableCollection<ProductTypesSalesChartItem> Items { get; } = new ObservableCollection<ProductTypesSalesChartItem>();

        public ICommand ReloadCommand { get; }

        public ProductTypesSalesChartViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);
            _startDate = _salesUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _salesUnits.Max(x => x.OrderInTakeDate);
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(FinishDate));
            RefreshItems();
        }

        private void RefreshItems()
        {
            var items = _salesUnits
                .Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate)
                .GroupBy(x => x.Product.ProductType)
                .Select(x => new ProductTypesSalesChartItem(x))
                .OrderByDescending(x => x.Sum)
                .ToList();

            Items.Clear();
            Items.AddRange(items);
        }
    }
}
