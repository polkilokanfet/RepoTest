using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationUnitsGroupsViewModel : SalesUnitsGroupsViewModel
    {
        private readonly Specification _specification;

        public SpecificationUnitsGroupsViewModel(IUnityContainer container, IEnumerable<SalesUnit> units, 
            IUnitOfWork unitOfWork, Specification specification) : base(container, units, unitOfWork, null)
        {
            _specification = specification;
        }

        protected override async void AddCommand_Execute()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == CommonOptions.User.Id);
            var unit = await Container.Resolve<ISelectService>().SelectItem(salesUnits);
            if (unit == null) return;
            var group = new SalesUnitsGroup(new[] {unit});
            group.Specification = new SpecificationWrapper(_specification);
            await RefreshPrice(group);
            Groups.Add(group);
        }

        public override async Task SaveChanges()
        {
            var removedIncl = Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems);
            var removed = Groups.RemovedItems.Concat(removedIncl).ToList();
            removed.ForEach(x =>
            {
                x.RejectChanges();
                x.Specification = null;
                x.AcceptChanges();
            });

            Groups.AcceptChanges();
            await UnitOfWork.SaveChangesAsync();
        }

        public override bool CanSaveChanges()
        {
            return Groups.IsValid && (Groups.IsChanged || Groups.RemovedItems.Any());
        }
    }
}