﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesReportViewModel : ViewModelBaseCanExportToExcel
    {
        private bool _isLoaded;
        private SalesReportUnit _selectedSalesReportUnit;

        public ObservableCollection<SalesReportUnit> Units { get; } = new ObservableCollection<SalesReportUnit>();

        public SalesReportUnit SelectedSalesReportUnit
        {
            get { return _selectedSalesReportUnit; }
            set
            {
                _selectedSalesReportUnit = value;
                ((DelegateCommand)EditFakeDataCommand).RaiseCanExecuteChanged();
            }
        }

        public bool TabEditVisibility => GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                                         GlobalAppProperties.User.RoleCurrent == Role.ReportMaker;

        public ICommand ReloadCommand { get; }
        public ICommand EditFakeDataCommand { get; }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public SalesReportViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);

            EditFakeDataCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<FakeDataView>(new NavigationParameters { { "units", SelectedSalesReportUnit.SalesUnits } });
                }, 
                () => SelectedSalesReportUnit != null);

            Load();
        }

        /// <summary>
        /// Загрузка отчета
        /// </summary>
        public void Load()
        {
            IsLoaded = false;
            Units.Clear();

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser()) 
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);
            var blocks = UnitOfWork.Repository<ProductBlock>().Find(x => true);
            var countryUnions = UnitOfWork.Repository<CountryUnion>().Find(x => true);

            Units.AddRange(groups.Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), blocks, countryUnions)));

            IsLoaded = true;
        }
    }
}