using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : UI.Modules.Sales.ViewModels.Groups.BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, 
        IGroupsViewModel<SalesUnit, SpecificationWrapper>
    {
        private SpecificationWrapper _specificationWrapper;
        /// <summary>
        /// Необходимо для отмены изменений
        /// </summary>
        private IValidatableChangeTrackingCollection<SalesUnitsWrappersGroup> _groupsToReject;

        public SpecificationUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser() && x.Specification == null);
            var unit = Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new SalesUnitsWrappersGroup(new List<SalesUnit> {unit});
            group.Specification = _specificationWrapper;
            var uetm = UnitOfWork.Repository<Company>().GetById(GlobalAppProperties.Actual.OurCompany.Id);
            group.Producer = new CompanyWrapper(uetm);
            RefreshPrice(group);
            Groups.Add(group);
            _groupsToReject.Add(group);
        }

        protected override List<SalesUnitsWrappersGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x.ToList())).ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, SpecificationWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _specificationWrapper = parentWrapper;
            //назначаем спецификацию всем юнитам
            Groups.ForEach(x => x.Specification = _specificationWrapper);
            _groupsToReject = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(Groups);
        }

        protected override DateTime GetPriceDate(SalesUnitsWrappersGroup @group)
        {
            var spec = @group.Specification;
            if (spec == null || spec.Date == default(DateTime)) return DateTime.Today;
            return spec.Date < DateTime.Today ? spec.Date : DateTime.Today;
        }

        public override void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //удаленные из спецификации группы
            var removed = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = Groups.RemovedItems.Concat(removed).ToList();
            removed.ForEach(x => { x.RejectChanges(); });
            removed.ForEach(x => { x.Specification = null; });
            removed.ForEach(x => { x.AcceptChanges(); });


            var added = GetAddedUnits().ToList();
            var modified = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.ModifiedItems).ToList();
            modified = modified.Concat(Groups.ModifiedItems).ToList();

            Groups.AcceptChanges();

            _groupsToReject.RejectChanges();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
        }

    }
}