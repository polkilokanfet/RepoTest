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
            return unitOfWork.Repository<PriceCalculation>()
                .Find(x => x.PriceCalculationItems.SelectMany(r => r.SalesUnits).Any(su => su.Project.Manager.IsAppCurrentUser()))
                .Select(x => new PriceCalculationLookup(x));
        }

        protected override IEnumerable<PriceCalculationLookup> GetActualLookups(Project project)
        {
            return AllLookups.Where(x => x.PriceCalculationItems.SelectMany(r => r.SalesUnits).Any(su => su.Project.Id == project.Id)).OrderBy(x => x.TaskCloseMoment);
        }

        protected override bool CanBeShown(PriceCalculation technicalRequrementsTask)
        {
            return Filter != null && technicalRequrementsTask.PriceCalculationItems.SelectMany(x => x.SalesUnits).Any(su => su.Project.Id == Filter.Id);
        }
    }
}