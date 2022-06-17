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
            return GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? unitOfWork.Repository<Tender>().GetAll().Select(tender => new TenderLookup(tender))
                : unitOfWork.Repository<Tender>().Find(tender => tender.Project.Manager.IsAppCurrentUser()).Select(tender => new TenderLookup(tender));
        }

        protected override IEnumerable<TenderLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(tenderLookup => tenderLookup.Project.Id == project.Id)
                .OrderByDescending(tenderLookup => tenderLookup.Entity.DateOpen);
        }

        protected override bool CanBeShown(Tender tender)
        {
            return Filter != null && Filter.Id == tender.Project.Id;
        }
    }
}