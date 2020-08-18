using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.PriorityReport
{
    public class PriorityReportViewModel : LoadableExportableViewModel
    {
        public ObservableCollection<PriorityReportGroup> Groups { get; } = new ObservableCollection<PriorityReportGroup>();

        public PriorityReportViewModel(IUnityContainer container) : base(container)
        {
        }

        private IEnumerable<PriorityReportGroup> _groups;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Order != null && (x.EndProductionDateCalculated >= DateTime.Today || x.SumNotPaid > 0.00001));
            _groups = salesUnits.GroupBy(x => x.Product.ProductType).Select(x => new PriorityReportGroup(x)).OrderByDescending(x => x.Cost);
        }

        protected override void AfterGetData()
        {
            Groups.Clear();
            Groups.AddRange(_groups);
        }
    }
}
