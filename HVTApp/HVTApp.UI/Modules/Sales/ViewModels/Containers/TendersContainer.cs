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
    public class TendersContainer : BaseContainerFilt<Tender, TenderLookup, SelectedTenderChangedEvent, AfterSaveTenderEvent, AfterRemoveTenderEvent, Project, SelectedProjectChangedEvent>
    {
        public TendersContainer(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<TenderLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Repository<Tender>().Find(x => x.Project.Manager.IsAppCurrentUser()).Select(x => new TenderLookup(x));
        }

        protected override IEnumerable<TenderLookup> GetActualLookups(Project project)
        {
            return AllLookups.Where(x => x.Project.Id == project.Id);
        }

        protected override bool CanBeShown(Tender tender)
        {
            return Filter != null && Filter.Id == tender.Project.Id;
        }
    }
}