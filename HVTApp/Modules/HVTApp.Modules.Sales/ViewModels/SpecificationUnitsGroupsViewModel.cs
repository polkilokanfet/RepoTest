using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : BaseGroupsViewModel<SalesUnitsWrappersGroup, SalesUnitsWrappersGroup, SalesUnit>, IGroupsViewModel<SalesUnit, SpecificationWrapper>
    {
        private SpecificationWrapper _specification;

        public SpecificationUnitsGroupsViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id && x.Specification == null);
            var unit = Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new SalesUnitsWrappersGroup(new[] {unit});
            group.Specification = _specification;
            RefreshPrice(group);
            Groups.Add(group);
        }

        public void Load(IEnumerable<SalesUnit> units, SpecificationWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew)
        {
            _specification = parentWrapper;
            UnitOfWork = unitOfWork;

            var groups = units.GroupBy(x => x, new SalesUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new SalesUnitsWrappersGroup(x)).ToList();

            if (isNew)
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(new List<SalesUnitsWrappersGroup>());
                groups.ForEach(x => Groups.Add(x));
                //назначаем спецификацию всем юнитам
                groups.ForEach(x => x.Specification = _specification);
            }
            else
            {
                Groups = new ValidatableChangeTrackingCollection<SalesUnitsWrappersGroup>(groups);
            }

            OnPropertyChanged(nameof(Groups));

            Groups.PropertyChanged += GroupsOnPropertyChanged;
            Groups.CollectionChanged += GroupsOnCollectionChanged;

            Groups.ForEach(RefreshPrice);
        }

        protected override DateTime GetPriceDate(SalesUnitsWrappersGroup grp)
        {
            throw new NotImplementedException();
        }

        public void AcceptChanges()
        {
            throw new NotImplementedException();
        }
    }
}