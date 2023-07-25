using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class PriceCalculationsContainer : BaseContainerViewModelWithFilterByProject<PriceCalculation, PriceCalculationLookup, SelectedPriceCalculationChangedEvent, AfterSavePriceCalculationEvent, AfterRemovePriceCalculationEvent, PriceCalculationView>
    {
        public PriceCalculationsContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
        }

        protected override IEnumerable<PriceCalculationLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? unitOfWork.Repository<PriceCalculation>().GetAll().Select(calculation => new PriceCalculationLookup(calculation))
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

        protected override bool CanBeShown(PriceCalculation calculation)
        {
            return this.SelectedProject != null && 
                   calculation.PriceCalculationItems
                       .SelectMany(item => item.SalesUnits)
                       .Any(salesUnit => salesUnit.Project.Id == this.SelectedProject.Id);
        }
    }
}