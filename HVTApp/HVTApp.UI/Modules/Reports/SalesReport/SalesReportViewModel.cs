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
using HVTApp.UI.Modules.Reports.ViewModels;
using HVTApp.UI.Modules.Reports.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    public class SalesReportViewModel : LoadableExportableExpandCollapseViewModel
    {
        private List<SalesReportUnit> _salesReportUnits;

        private SalesReportUnit _selectedSalesReportUnit;
        private DateTime _startDate;
        private DateTime _finishDate;

        public ObservableCollection<SalesReportUnit> Units { get; } = new ObservableCollection<SalesReportUnit>();

        public SalesReportUnit SelectedSalesReportUnit
        {
            get { return _selectedSalesReportUnit; }
            set
            {
                _selectedSalesReportUnit = value;
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                RefreshUnits();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (Equals(_finishDate, value)) return;
                _finishDate = value;
                RefreshUnits();
            }
        }

        public bool TabEditVisibility => GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                                         GlobalAppProperties.User.RoleCurrent == Role.ReportMaker;

        public SalesReportViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }


            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);
            var countryUnions = UnitOfWork.Repository<CountryUnion>().Find(x => true);

            _salesReportUnits = groups
                .Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), countryUnions, x.First().ActualPriceCalculationItem(UnitOfWork)))
                .ToList();
        }

        protected override void AfterGetData()
        {
            _startDate = _salesReportUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _salesReportUnits.Max(x => x.OrderInTakeDate);
            RefreshUnits();
            Container.Resolve<IMessageService>().ShowOkMessageDialog("Загрузка данных", "Загрузка отчета завершена.");
        }

        private void RefreshUnits()
        {
            Units.Clear();
            Units.AddRange(_salesReportUnits.Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate));
        }
    }
}