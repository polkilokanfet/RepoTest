using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : UI.Modules.Sales.ViewModels.Groups.BaseGroupsViewModel<ProjectUnitsGroup, ProjectUnitsGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, 
        IGroupsViewModel<SalesUnit, SpecificationWrapper>
    {
        private SpecificationWrapper _specificationWrapper;
        /// <summary>
        /// Необходимо для отмены изменений
        /// </summary>
        private IValidatableChangeTrackingCollection<ProjectUnitsGroup> _groupsToReject;

        private List<ProjectUnitsGroup> _removedUnits = new List<ProjectUnitsGroup>();

        public SpecificationUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser() && x.Specification == null);
            var unit = Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new ProjectUnitsGroup(new List<SalesUnit> {unit});
            group.Specification = new SpecificationSimpleWrapper(_specificationWrapper.Model);
            var uetm = UnitOfWork.Repository<Company>().GetById(GlobalAppProperties.Actual.OurCompany.Id);
            group.Producer = new CompanySimpleWrapper(uetm);
            RefreshPrice(group);
            Groups.Add(group);
            _groupsToReject.Add(group);
        }

        protected override void RemoveCommand_Execute()
        {
            if (CanRemoveGroup(Groups.SelectedGroup))
            {
                if (Container.Resolve<IMessageService>().ConfirmationDialog("Удаление", "Вы уверены, что хотите удалить это оборудование?", defaultNo: true) == false)
                {
                    return;
                }

                _removedUnits.Add(Groups.SelectedGroup);
                RemoveGroup(Groups.SelectedGroup);
                Groups.SelectedGroup = null;
            }
        }

        protected override List<ProjectUnitsGroup> GetGroups(IEnumerable<SalesUnit> units)
        {
            return units
                .GroupBy(salesUnit => salesUnit, new SalesUnitsGroupsComparer())
                .OrderBy(x => x.Key, new ProductCostComparer())
                .Select(x => new ProjectUnitsGroup(x.ToList()))
                .ToList();
        }

        public void Load(IEnumerable<SalesUnit> units, SpecificationWrapper specificationWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            Load(units, unitOfWork, isNew);
            _specificationWrapper = specificationWrapper;
            //назначаем спецификацию всем юнитам
            var specificationSimpleWrapper = new SpecificationSimpleWrapper(_specificationWrapper.Model);
            Groups.ForEach(x => x.Specification = specificationSimpleWrapper);
            _groupsToReject = new ValidatableChangeTrackingCollection<ProjectUnitsGroup>(Groups);
        }

        protected override DateTime GetPriceDate(ProjectUnitsGroup @group)
        {
            return @group.RealizationDateCalculated;
            var spec = @group.Specification;
            if (spec == null || spec.Model.Date == default(DateTime)) return DateTime.Today;
            return spec.Model.Date < DateTime.Today ? spec.Model.Date : DateTime.Today;
        }

        public override void AcceptChanges()
        {
            var eventAggregator = Container.Resolve<IEventAggregator>();

            //удаленные из спецификации группы
            _removedUnits.ForEach(x => { x.RejectChanges(); });
            _removedUnits.ForEach(x => { x.Specification = null; });
            _removedUnits.ForEach(x => { x.AcceptChanges(); });

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