using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
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

        protected List<SalesUnit> SalesUnitsFiltered
        {
            get
            {
                var salesUnits = SalesUnits.Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate);
                if (Parameters.Any())
                    salesUnits = salesUnits.Where(x => Parameters.AllContainsIn(x.Product.ProductBlock.Parameters));
                return salesUnits.ToList();
            }
        }

        private DateTime _startDate;
        private DateTime _finishDate;
        private int _year = DateTime.Today.Year;
        private Parameter _selectedParameter;

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
                ((DelegateCommand)GetDataByYearCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();


        public ICommand AddParameterCommand { get; }
        public ICommand RemoveParameterCommand { get; }

        public ObservableCollection<Parameter> Parameters { get; } = new ObservableCollection<Parameter>();

        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                _selectedParameter = value;
                ((DelegateCommand)RemoveParameterCommand).RaiseCanExecuteChanged();
            }
        }


        public ICommand ReloadCommand { get; }
        public ICommand GetDataByYearCommand { get; }

        protected SalesChartViewModel(IUnityContainer container) : base(container)
        {
            AddParameterCommand = new DelegateCommand(
                () =>
                {
                    var parameters = UnitOfWork.Repository<Parameter>().GetAll();
                    var parameter = Container.Resolve<ISelectService>().SelectItem(parameters);
                    if (parameter != null)
                    {
                        Parameters.Add(parameter);
                    }
                });

            RemoveParameterCommand = new DelegateCommand(
                () =>
                {
                    Parameters.Remove(SelectedParameter);
                    SelectedParameter = null;
                },
                () => SelectedParameter != null);

            Parameters.CollectionChanged += (sender, args) => { this.RefreshItems(); };

            GetDataByYearCommand = new DelegateCommand(
                () =>
                {
                    StartDate = new DateTime(Year, 1, 1);
                    FinishDate = new DateTime(Year, 12, 31);
                },
                () => Year > 1899 && Year < 2201);
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

        protected void RefreshItems()
        {
            var items = GetItems();
            Items.Clear();
            Items.AddRange(items);
            OnPropertyChanged(nameof(SumOfSalesUnits));
        }
    }
}
