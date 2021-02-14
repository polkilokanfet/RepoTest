using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.SalesReport;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    public class CommonInfoViewModel : LoadableExportableExpandCollapseViewModel
    {
        private IEnumerable<CommonInfoUnitGroup> _salesReportUnits;
        public ObservableCollection<CommonInfoUnitGroup> Units { get; } = new ObservableCollection<CommonInfoUnitGroup>();

        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-2);
        public DateTime FinishDate { get; set; } = DateTime.Today.AddMonths(2);

        public CommonInfoViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                //? UnitOfWork.Repository<SalesUnit>().Find(salesUnit => !salesUnit.IsRemoved && !salesUnit.IsLoosen && salesUnit.Project.Manager.IsAppCurrentUser())
                //: UnitOfWork.Repository<SalesUnit>().Find(salesUnit => !salesUnit.IsRemoved && !salesUnit.IsLoosen);
                ? UnitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.OrderInTakeDate.BetweenDates(StartDate, FinishDate) && !salesUnit.IsRemoved && !salesUnit.IsLoosen && salesUnit.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.OrderInTakeDate.BetweenDates(StartDate, FinishDate) && !salesUnit.IsRemoved && !salesUnit.IsLoosen);

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(salesUnit => salesUnit.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(salesUnit => salesUnit.ProductsIncluded.Contains(productIncluded));
            }

            var tenders = UnitOfWork.Repository<Tender>().GetAll();

            var groups = salesUnits.GroupBy(salesUnit => salesUnit, new SalesUnitsCommonInfoComparer());
            _salesReportUnits = groups.Select(x => new CommonInfoUnitGroup(x, tenders.Where(tender => Equals(x.Key.Project, tender.Project)), UnitOfWork)).ToList();
        }

        protected override void AfterGetData()
        {
            Units.Clear();
            Units.AddRange(_salesReportUnits);
        }
    }
}