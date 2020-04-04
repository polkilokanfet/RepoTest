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

namespace HVTApp.UI.Modules.Reports.SalesCharts
{
    public abstract class SalesChartViewModel<T> : ViewModelBase 
        where T: SalesChartItem 
    {
        protected List<SalesUnit> SalesUnits;
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

        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

        public ICommand ReloadCommand { get; }

        protected SalesChartViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        protected virtual List<SalesUnit> GetSalesUnits()
        {
            return GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            SalesUnits = GetSalesUnits();
            _startDate = SalesUnits.Min(x => x.OrderInTakeDate);
            _finishDate = SalesUnits.Max(x => x.OrderInTakeDate);
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(FinishDate));
            RefreshItems();
        }

        protected abstract List<T> GetItems();

        private void RefreshItems()
        {
            var items = GetItems();
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
