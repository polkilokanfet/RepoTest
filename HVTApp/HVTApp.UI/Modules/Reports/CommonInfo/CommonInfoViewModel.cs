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
        private IEnumerable<CommonInfoUnitGroup> _salesReportUnitsTemp;

        private readonly ObservableCollection<CommonInfoUnitGroup> _salesReportUnits = new ObservableCollection<CommonInfoUnitGroup>();
        public IEnumerable<CommonInfoUnitGroup> Units => _salesReportUnits;

        public string FacilityNameCondition { get; set; } = null;

        public CommonInfoViewModel(IUnityContainer container) : base(container, false)
        {
            this.IsLoaded = GlobalAppProperties.UserIsManager == false;
        }

        protected override void GetData()
        {
            _salesReportUnitsTemp = null;

            UnitOfWork = Container.Resolve<IUnitOfWork>();

            List<SalesUnit> salesUnits;

            if (string.IsNullOrWhiteSpace(this.FacilityNameCondition))
            {
                salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllNotRemovedNotLoosen().ToList();
            }
            else
            {
                var facilities = UnitOfWork.Repository<Facility>()
                    .Find(facility => facility.Name.ToLower().Contains(FacilityNameCondition.Trim().ToLower()))
                    .ToList();

                salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>())
                    .GetForCommonInfo(facilities)
                    .Where(x => x.IsLoosen == false)
                    .ToList();
            }

            if (GlobalAppProperties.UserIsManager)
                salesUnits = salesUnits.Where(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()).ToList();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(salesUnit => salesUnit.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(salesUnit => salesUnit.ProductsIncluded.Contains(productIncluded));
            }

            var tenders = UnitOfWork.Repository<Tender>().GetAll();

            var groups = salesUnits.GroupBy(salesUnit => salesUnit, new SalesUnitsCommonInfoComparer());
            _salesReportUnitsTemp = groups
                .Select(x => new CommonInfoUnitGroup(x, tenders.Where(tender => Equals(x.Key.Project, tender.Project)), UnitOfWork))
                .OrderBy(x => x.OrderInTakeDate)
                .ToList();
        }

        protected override void AfterGetData()
        {
            _salesReportUnits.Clear();
            if (_salesReportUnitsTemp != null && _salesReportUnitsTemp.Any())
                _salesReportUnits.AddRange(_salesReportUnitsTemp);
        }
    }
}