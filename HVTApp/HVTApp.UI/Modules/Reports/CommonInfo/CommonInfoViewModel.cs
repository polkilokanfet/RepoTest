using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    public class CommonInfoViewModel : LoadableExportableExpandCollapseViewModel
    {
        private IEnumerable<CommonInfoUnitGroup> _salesReportUnits;
        public ObservableCollection<CommonInfoUnitGroup> Units { get; } = new ObservableCollection<CommonInfoUnitGroup>();

        public CommonInfoViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = GlobalAppProperties.UserIsManager
                ? ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllNotRemovedNotLoosen().Where(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()).ToList()
                : ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllNotRemovedNotLoosen().ToList();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(salesUnit => salesUnit.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(salesUnit => salesUnit.ProductsIncluded.Contains(productIncluded));
            }

            var tenders = UnitOfWork.Repository<Tender>().GetAll();

            var groups = salesUnits.GroupBy(salesUnit => salesUnit, new SalesUnitsCommonInfoComparer());
            _salesReportUnits = groups.Select(x => new CommonInfoUnitGroup(x, tenders.Where(tender => Equals(x.Key.Project, tender.Project)), UnitOfWork))
                .OrderBy(x => x.OrderInTakeDate).ToList();
        }

        protected override void AfterGetData()
        {
            Units.Clear();
            Units.AddRange(_salesReportUnits);
        }
    }
}