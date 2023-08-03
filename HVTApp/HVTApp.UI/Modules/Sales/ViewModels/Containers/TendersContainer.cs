using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class TendersContainer : BaseContainerViewModelWithFilterByProject<Tender, TenderLookup, AfterSaveTenderEvent, AfterRemoveTenderEvent, TenderDetailsView>
    {
        public TendersContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
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
            return this.SelectedProject != null && 
                   this.SelectedProject.Id == tender.Project.Id;
        }

        public override void EditSelectedItem()
        {
            var tenderViewModel = new TenderViewModel(Container, this.SelectedItem.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }
    }
}