using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : 
        BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit, AfterSaveSalesUnitEvent, AfterRemoveSalesUnitEvent>, 
        IGroupsViewModel<SalesUnit, SpecificationWrapper>
    {
        private SpecificationWrapper _specificationWrapper;
        /// <summary>
        /// Необходимо для отмены изменений
        /// </summary>
        private IValidatableChangeTrackingCollection<SalesUnitsWrappersGroup> _originGroups;

        public SpecificationUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id && x.Specification == null);
            var unit = Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new SalesUnitsWrappersGroup(new[] {unit});
            group.Specification = _specificationWrapper;
            RefreshPrice(group);
            Groups.Add(group);
        }

        public void Load(IEnumerable<SalesUnit> units, SpecificationWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            _specificationWrapper = parentWrapper;
            UnitOfWork = unitOfWork;

            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x)).ToList();

            if (isNew)
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(new List<SalesUnitsWrappersGroup>());
                groups.ForEach(x => Groups.Add(x));
                //назначаем спецификацию всем юнитам
                groups.ForEach(x => x.Specification = _specificationWrapper);
            }
            else
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(groups);
            }

            _originGroups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(Groups);

            OnPropertyChanged(nameof(Groups));

            Groups.PropertyChanged += GroupsOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

            Groups.ForEach(RefreshPrice);
        }

        protected override DateTime GetPriceDate(SalesUnitsWrappersGroup grp)
        {
            if (_specificationWrapper.Date == default(DateTime)) return DateTime.Today;
            return _specificationWrapper.Date < DateTime.Today ? _specificationWrapper.Date : DateTime.Today;
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

            _originGroups.RejectChanges();

            added.Concat(modified.Select(x => x.Model)).Distinct().ForEach(x => eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Publish(x));
        }

    }
}