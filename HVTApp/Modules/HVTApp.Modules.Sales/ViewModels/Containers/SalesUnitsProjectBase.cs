using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsProjectBase : SalesUnitsContainerBase<Project, SelectedProjectChangedEvent>
    {
        public SalesUnitsProjectBase(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<SalesUnitLookup> GetActualLookups(Project project)
        {
            return AllLookups.Where(x => x.Project.Id == project.Id);
        }

        protected override bool CanBeShown(SalesUnitLookup salesUnitLookup)
        {
            return Filt != null && Filt.Id == salesUnitLookup.Entity.Project.Id;
        }
    }
}