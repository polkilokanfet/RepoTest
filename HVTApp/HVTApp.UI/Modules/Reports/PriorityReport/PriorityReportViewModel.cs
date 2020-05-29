using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.PriorityReport
{
    public class PriorityReportViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {
        public ObservableCollection<PriorityReportGroup> Groups { get; } = new ObservableCollection<PriorityReportGroup>();

        public ICommand ReloadCommand { get; }

        public PriorityReportViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        public void Load()
        {
            Groups.Clear();
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Order != null && (x.EndProductionDateCalculated >= DateTime.Today || x.SumNotPaid > 0.00001));
            //var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Order != null && DateTime.Today <= x.EndProductionDateCalculated.AddDays(-GlobalAppProperties.Actual.StandartTermFromPickToEndProduction));
            Groups.AddRange(salesUnits.GroupBy(x => x.Product.ProductType).Select(x => new PriorityReportGroup(x)).OrderByDescending(x => x.Cost));
        }
    }
}
