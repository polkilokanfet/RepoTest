using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : SalesUnitsGroupsViewModel
    {
        private readonly SpecificationWrapper _specification;

        public SpecificationUnitsGroupsViewModel(IUnityContainer container, IEnumerable<SalesUnit> units, 
            IUnitOfWork unitOfWork, SpecificationWrapper specification) : base(container, units, unitOfWork, null)
        {
            _specification = specification;
            //назначаем спецификацию всем юнитам
            Groups.ForEach(x => x.Specification = specification);
        }

        protected override void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id && x.Specification == null);
            var unit = Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new SalesUnitsGroup(new[] {unit});
            group.Specification = _specification;
            RefreshPrice(group);
            Groups.Add(group);
        }

        public override bool CanSaveChanges()
        {
            return Groups.IsValid && (Groups.IsChanged || Groups.RemovedItems.Any());
        }
    }
}