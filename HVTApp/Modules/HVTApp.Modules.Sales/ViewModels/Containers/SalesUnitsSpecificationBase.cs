using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsSpecificationBase : SalesUnitsContainerBase<Specification, SelectedSpecificationChangedEvent>
    {
        public SalesUnitsSpecificationBase(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<SalesUnitLookup> GetActualLookups(Specification specification)
        {
            return AllLookups.Where(x => x.Specification?.Id == specification.Id);
        }

        protected override bool CanBeShown(SalesUnit salesUnit)
        {
            return Filter != null && Filter.Id == salesUnit.Specification.Id;
        }
    }
}