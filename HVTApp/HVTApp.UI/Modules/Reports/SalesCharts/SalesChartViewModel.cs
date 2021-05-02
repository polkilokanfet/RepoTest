using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.SalesCharts
{
    public abstract class SalesChartViewModel<T> : ViewModelBase 
        where T: SalesChartItem 
    {
        private DateTime _startDate;
        private DateTime _finishDate;
        private int _year = DateTime.Today.Year;
        private Parameter _selectedParameter;

        private List<SalesUnit> _salesUnits;

        protected List<SalesUnit> SalesUnitsFiltered
        {
            get
            {
                var salesUnits = _salesUnits.Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate);
                if (Parameters.Any())
                    salesUnits = salesUnits.Where(x => Parameters.AllContainsIn(x.Product.ProductBlock.Parameters));
                return salesUnits.ToList();
            }
        }

        public abstract string Title { get; }
        public string TitleItem => Items.FirstOrDefault()?.Title ?? "no data";

        public double SumOfSalesUnits => SalesUnitsFiltered.Sum(x => x.Cost);

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                RefreshItems();
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                (GetDataByYearCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();


        public DelegateLogCommand AddParameterCommand { get; }
        public DelegateLogCommand RemoveParameterCommand { get; }

        public ObservableCollection<Parameter> Parameters { get; } = new ObservableCollection<Parameter>();

        public Parameter SelectedParameter
        {
            get => _selectedParameter;
            set
            {
                _selectedParameter = value;
                RemoveParameterCommand.RaiseCanExecuteChanged();
            }
        }


        public DelegateLogCommand ReloadCommand { get; }
        public DelegateLogCommand GetDataByYearCommand { get; }

        protected SalesChartViewModel(IUnityContainer container) : base(container)
        {
            AddParameterCommand = new DelegateLogCommand(
                () =>
                {
                    var parameters = UnitOfWork.Repository<Parameter>().GetAll();
                    var parameter = Container.Resolve<ISelectService>().SelectItem(parameters);
                    if (parameter != null)
                    {
                        Parameters.Add(parameter);
                    }
                });

            RemoveParameterCommand = new DelegateLogCommand(
                () =>
                {
                    Parameters.Remove(SelectedParameter);
                    SelectedParameter = null;
                },
                () => SelectedParameter != null);

            Parameters.CollectionChanged += (sender, args) => { this.RefreshItems(); };

            GetDataByYearCommand = new DelegateLogCommand(
                () =>
                {
                    StartDate = new DateTime(Year, 1, 1);
                    FinishDate = new DateTime(Year, 12, 31);
                },
                () => Year > 1899 && Year < 2201);
            ReloadCommand = new DelegateLogCommand(() => Load(true));
            Load();
        }

        protected virtual List<SalesUnit> GetSalesUnits()
        {
            return GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? SalesUnitsContainer.SalesUnits.Where(x => x.IsWon && x.Project.Manager.IsAppCurrentUser()).ToList()
                : SalesUnitsContainer.SalesUnits.Where(x => x.IsWon).ToList();
        }

        private void Load(bool isReload = false)
        {
            if (isReload)
            {
                UnitOfWork = Container.Resolve<IUnitOfWork>();
            }

            if (isReload || SalesUnitsContainer.SalesUnits == null)
            {
                SalesUnitsContainer.SalesUnits = UnitOfWork.Repository<SalesUnit>().GetAll();
            }
            _salesUnits = GetSalesUnits();

            _startDate = _salesUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _salesUnits.Max(x => x.OrderInTakeDate);
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(FinishDate));
            RefreshItems();
        }

        protected abstract List<T> GetItems();

        protected void RefreshItems()
        {
            var items = GetItems();
            Items.Clear();
            Items.AddRange(items);
            OnPropertyChanged(nameof(SumOfSalesUnits));
        }
    }
}
