using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel : ViewModelBase
    {
        public ProjectsContainer Projects { get; }
        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }
        public SalesUnitsProjectBase SalesUnits { get; }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {

            Projects = container.Resolve<ProjectsContainer>();
            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();
            SalesUnits = container.Resolve<SalesUnitsProjectBase>();

            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => Projects.SelectedItem != null);
            RemoveProjectCommand = new DelegateCommand(async () => await Projects.RemoveSelectedItemTask(), () => Projects.SelectedItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute, () => Projects.SelectedItem != null);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(async () => await Offers.RemoveSelectedItemTask(), () => Offers.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => Projects.SelectedItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => Projects.SelectedItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(async () => await Tenders.RemoveSelectedItemTask(), () => Tenders.SelectedItem != null);

            StructureCostsCommand = new DelegateCommand(StructureCostsCommand_Execute, () => Projects.SelectedItem != null);

            #endregion

            #region Subscribe to Events

            //подписка на выбор сущностей
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<SelectedProjectChangedEvent>().Subscribe(project => ProjectRaiseCanExecuteChanged());
            eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewSpecificationCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)StructureCostsCommand).RaiseCanExecuteChanged();
            OfferRaiseCanExecuteChanged();
            TenderRaiseCanExecuteChanged();
        }

        private void OfferRaiseCanExecuteChanged()
        {
            ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
        }

        private void TenderRaiseCanExecuteChanged()
        {
            ((DelegateCommand)NewTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveTenderCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
