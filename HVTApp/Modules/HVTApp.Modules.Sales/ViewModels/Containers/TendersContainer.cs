using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TendersContainer : BaseContainerFilt<Tender, TenderLookup, SelectedTenderChangedEvent, AfterSaveTenderEvent, AfterRemoveTenderEvent, Project, SelectedProjectChangedEvent>
    {
        public TendersContainer(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<Tender> GetItems(IUnitOfWorkDisplay unitOfWork)
        {
            return unitOfWork.Repository<Tender>().Find(x => x.Project.Manager.IsAppCurrentUser());
        }


        protected override IEnumerable<TenderLookup> GetActualLookups(Project project)
        {
            return AllItems.Where(x => x.Project.Id == project.Id).Select(x => new TenderLookup(x));
        }
    }
}