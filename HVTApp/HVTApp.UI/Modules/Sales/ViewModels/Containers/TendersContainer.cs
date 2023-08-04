using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class TendersContainer : BaseContainerViewModelWithFilterByProject<Tender, TenderLookup, AfterSaveTenderEvent, AfterRemoveTenderEvent, TenderDetailsView>
    {
        public ICommand OpenTenderLinkCommand { get; }
        public ICommand NewTenderCommand { get; }

        public TendersContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
            #region OpenTenderLinkCommand

            OpenTenderLinkCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(this.SelectedItem.Link) == false)
                        Process.Start(this.SelectedItem.Link);
                },
                () => this.SelectedItem != null);

            this.SelectedItemChangedEvent += lookup => ((DelegateLogCommand)OpenTenderLinkCommand).RaiseCanExecuteChanged();

            #endregion

            #region NewTenderCommand

            NewTenderCommand = new DelegateLogCommand(
                () =>
                {
                    var tenderViewModel = new TenderViewModel(container, vm.SelectedProjectItem.Project);
                    container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
                },
                () => vm.SelectedProjectItem != null);
            vm.SelectedProjectItemChanged += item => ((DelegateLogCommand) NewTenderCommand).RaiseCanExecuteChanged();

            #endregion
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