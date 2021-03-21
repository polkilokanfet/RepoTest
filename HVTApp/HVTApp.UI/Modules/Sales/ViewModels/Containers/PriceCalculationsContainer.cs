using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class PriceCalculationsContainer : BaseContainerFilt<PriceCalculation, PriceCalculationLookup, SelectedPriceCalculationChangedEvent, AfterSavePriceCalculationEvent, AfterRemovePriceCalculationEvent, Project, SelectedProjectChangedEvent>
    {
        public PriceCalculationsContainer(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<PriceCalculationLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? unitOfWork.Repository<PriceCalculation>().GetAll().Select(x => new PriceCalculationLookup(x))
                : unitOfWork.Repository<PriceCalculation>()
                    .Find(priceCalculation => priceCalculation.PriceCalculationItems.SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits).Any(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()))
                    .Select(priceCalculation => new PriceCalculationLookup(priceCalculation));
        }

        protected override IEnumerable<PriceCalculationLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(priceCalculationLookup => priceCalculationLookup.PriceCalculationItems.SelectMany(priceCalculationItemLookup => priceCalculationItemLookup.SalesUnits).Any(salesUnitLookup => salesUnitLookup.Project.Id == project.Id))
                .OrderByDescending(priceCalculationLookup => priceCalculationLookup.Entity.TaskCloseMoment);
        }

        protected override bool CanBeShown(PriceCalculation technicalRequrementsTask)
        {
            return Filter != null && technicalRequrementsTask.PriceCalculationItems.SelectMany(x => x.SalesUnits).Any(su => su.Project.Id == Filter.Id);
        }
    }
}